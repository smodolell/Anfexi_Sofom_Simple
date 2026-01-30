using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class TablaAmortizacionService(
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService
) : ITablaAmortizacionService
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public async Task<FrecuenciaPagoDto> ObtenerFrecuenciaPagoAsync(int idAgencia, int idPeriodicidad)
    {
        var result = await _dbContext.Database
            .SqlQueryRaw<FrecuenciaPagoDto>(
                $"EXEC usp_GetFrecuenciaPago @IdAgencia={idAgencia}, @IdPeriodicidad={idPeriodicidad}")
            .ToListAsync();

        return result.FirstOrDefault() ?? throw new InvalidOperationException("No se encontró frecuencia de pago");
    }

    public async Task<List<TablaAmortizaItemDto>> GenerarTablaAmortizacionAsync(
        int idPlan,
        int idPlazo,
        int idAgencia,
        int idPeriodicidad,
        decimal montoSolicitar)
    {
        // 1. Obtener frecuencia de pago
        var frecuencia = await ObtenerFrecuenciaPagoAsync(idAgencia, idPeriodicidad);

        // 2. Obtener datos del plan
        var plan = await _dbContext.COT_Plan
            .Include(i => i.COT_PlanTasa)
                .ThenInclude(t => t.IdTasaNavigation)
            .SingleAsync(r => r.IdPlan == idPlan);

        var valorTasa = plan.COT_PlanTasa.First().IdTasaNavigation.Valor ?? 0;
        var tasa = Convert.ToDouble(valorTasa);
        var tasaIva = Convert.ToDouble(16);

        // 3. Obtener plazo y calcular número de pagos
        var plazoEntity = await _dbContext.COT_Plazo.SingleAsync(r => r.IdPlazo == idPlazo);
        var plazoAux = plazoEntity.Plazo;

        var periodicidad = await _dbContext.SB_Periodicidad
            .SingleAsync(x => x.IdPeriodicidad == frecuencia.IdPeriodicidad);

        var numeroPagosFijos = plazoAux != 0
            ? (plazoAux / 12) * periodicidad.NroPagosAnio
            : 1;

        var plazo = numeroPagosFijos == 0 ? 1 : numeroPagosFijos;
        var montoServicioMedico = 0.0m;

        // 4. Preparar parámetros para el stored procedure
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("MontoSolicitado", montoSolicitar.ToString("0.00", new System.Globalization.CultureInfo("es-MX"))),
            new SqlParameter("Tasa", tasa.ToString("0.00", new System.Globalization.CultureInfo("es-MX"))),
            new SqlParameter("TasaIva", tasaIva),
            new SqlParameter("Plazo", plazo),
            new SqlParameter("IdPeridicidad", frecuencia.IdPeriodicidad),
            new SqlParameter("FechaTA", frecuencia.DiaInicio.ToString("yyyyMMdd")),
            new SqlParameter("ServicioMedico", montoServicioMedico.ToString("0.00", new System.Globalization.CultureInfo("es-MX")))
        };

        // 5. Ejecutar stored procedure
        var tablaAmortizacion = await _dbContext.Database
            .SqlQueryRaw<TablaAmortizaItemDto>(
                "EXEC usp_GeneraTablaAmortizaSimuladorNew @MontoSolicitado, @Tasa, @TasaIva, @Plazo, @IdPeridicidad, @FechaTA, @ServicioMedico",
                parameters.ToArray())
            .ToListAsync();

        return tablaAmortizacion;
    }



    public async Task<int> GuardarTablaAmortizacionAsync(
       int idCotizador,
       List<TablaAmortizaItemDto> tablaAmortizacionDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {

            var cot = await _dbContext.COT_Cotizador
                .Include(c => c.IdPlanNavigation)
                .FirstAsync(r => r.IdCotizador == idCotizador);

            var plazo = await _dbContext.COT_Plazo
                           .Where(r => r.IdPlazo == cot.IdPlazo)
                           .Select(r => r.Plazo)
                           .FirstAsync();

            // 2. Obtener clave consecutiva usando el servicio
            var consecutivo = await _consecutivoService
                .ObtenerSiguienteConsecutivoAsync("COT_TablaAmortizacion");

            if (!consecutivo.Success)
            {
                throw new InvalidOperationException($"Error al obtener consecutivo: {consecutivo.ErrorMessage}");
            }

            // Ajustar clave según la lógica original
            var clave = consecutivo.ConsecutivoGenerado - plazo;

            // 3. Mapear y guardar registros
            var tablaEntities = new List<COT_TablaAmortiza>();
            var fechaAlta = DateTime.Now;

            foreach (var itemDto in tablaAmortizacionDto)
            {
                var entity = new COT_TablaAmortiza
                {
                    IdTablaAmortiza = clave + itemDto.NoPago,
                    IdTipoTabla = 1,
                    IdCotizador = idCotizador,
                    NoPago = itemDto.NoPago,
                    FecVencimiento = itemDto.FecVencimiento,
                    Capital = itemDto.Capital,
                    Interes = itemDto.Interes,
                    IVA = itemDto.IVA,
                    Total = itemDto.Total,
                    SaldoInicial = itemDto.SaldoInicial,
                    SaldoFinal = itemDto.SaldoFinal,
                    //ServicioMedico = itemDto.ServicioMedico,
                    RequiereCalculo = false,
                    VersionTabla = 0,
                    Procesado = false,
                    EsValorResidual = false,

                };
                tablaEntities.Add(entity);
            }

            // 4. Guardar en lote
            await _dbContext.COT_TablaAmortiza.AddRangeAsync(tablaEntities);
            await _dbContext.SaveChangesAsync();

            await _consecutivoService.ReiniciarConsecutivoAsync("COT_TablaAmortizacion", tablaEntities.Max(m => m.IdTablaAmortiza));

            await transaction.CommitAsync();

            return tablaEntities.Count;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

