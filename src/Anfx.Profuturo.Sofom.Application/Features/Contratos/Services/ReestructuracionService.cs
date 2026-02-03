using Anfx.Profuturo.Sofom.Application.Features.Contratos.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Anfx.Profuturo.Sofom.Application.Features.Contratos.Services;

public class ReestructuracionService(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IDatabaseServices databaseService,
    IFolioService folioService,
    ILogger<ReestructuracionService> logger
) : IReestructuracionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseServices _databaseService = databaseService;
    private readonly IFolioService _folioService = folioService;
    private readonly ILogger<ReestructuracionService> _logger = logger;


    public async Task<Result<ReestructuracionResult>> IniciarReestructuracion(string contratos, int idAsesor)
    {

        var listaContratos = contratos.Split('_')
            .Where(c => !string.IsNullOrEmpty(c))
            .ToList();

        if (!listaContratos.Any())
        {
            _logger.LogWarning("⚠️ No se recibieron contratos válidos");
            return Result.Error("No se recibieron contratos válidos");
        }
        _logger.LogDebug("📋 Contratos a procesar: {Count}", listaContratos.Count);

        var (listaIdRees, idSolicitud) = await EjecutarSpsConCommitPropioAsync(contratos);


        if (idSolicitud == 0)
            return Result.Error("asdasdas");


        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _logger.LogInformation("✅ Todos los contratos procesados correctamente");

            var idc = listaIdRees.LastOrDefault();

            _logger.LogDebug("Buscando última reestructuración del contrato ID: {IdContrato}", idc);

            var solReestructurado = await _dbContext.RC_Reestructuracion
                .Where(r => r.IdContrato == idc)
                .OrderByDescending(r => r.IdReestructuracion)
                .FirstOrDefaultAsync();

            if (solReestructurado == null)
            {
                _logger.LogError("No se encontró reestructuración para el último contrato");
                return Result.Error("No se encontró reestructuración para el último contrato");
            }

            _logger.LogDebug("Reestructuración encontrada: ID {IdReestructuracion}",
                solReestructurado.IdReestructuracion);

            // 4. Actualizar reestructuraciones intermedias (copia exacta del ciclo)
            _logger.LogDebug("🔄 Actualizando {Count} reestructuraciones intermedias", listaIdRees.Count);

            var reestructuraciones = await _dbContext.RC_Reestructuracion
                .Where(r => listaIdRees.Contains(r.IdReestructuracion))
                .ToListAsync();

            foreach (var reestructuracion in reestructuraciones)
            {
                reestructuracion.IdSolicitudNew = solReestructurado.IdSolicitudNew;
                reestructuracion.Activo = false;
            }
            // 5. Calcular sumatorias (copia exacta del ciclo)
            decimal sumaCapitalLiberar = 0;
            decimal sumaSaldoInsoluto = 0;
            decimal sumaNewCapital = 0;

            _logger.LogDebug("🧮 Calculando sumatorias para {Count} reestructuraciones", listaIdRees.Count);

            foreach (var item in listaIdRees)
            {
                var data = await _dbContext.RC_Reestructuracion
                    .SingleOrDefaultAsync(r => r.IdReestructuracion == item);

                if (data != null)
                {
                    var cal = await _databaseService.usp_SaldoLiquidacionAsync(data.IdContrato, DateTime.Now);
                    var saldoInsolutotemp = cal.FirstOrDefault();
                    if (saldoInsolutotemp == null)
                    {
                        throw new Exception("NO se pudo calcular el Saldo Liquidacion");
                    }
                    data.SaldoInsoluto = saldoInsolutotemp.Saldo;

                    _dbContext.RC_Reestructuracion.Update(data);

                    sumaCapitalLiberar += data.CapitalALibrerar ?? 0;
                    sumaSaldoInsoluto += data.SaldoInsoluto ?? 0;
                    sumaNewCapital += data.CapitalNew;

                    _logger.LogDebug("📊 Acumulado - Reestructuración {Id}: CapitalLiberar={CapitalLiberar}, Saldo={Saldo}, CapitalNew={CapitalNew}",
                        item, data.CapitalALibrerar, data.SaldoInsoluto, data.CapitalNew);
                }
            }

            _logger.LogDebug("📈 Sumatorias finales: CapitalLiberar={SumaCapitalLiberar}, SaldoInsoluto={SumaSaldo}, CapitalNew={SumaCapitalNew}",
                sumaCapitalLiberar, sumaSaldoInsoluto, sumaNewCapital);

            // 6. Obtener ID de solicitud (copia exacta)
            idSolicitud = solReestructurado.IdSolicitudNew ?? 0;
            _logger.LogInformation("📋 ID Solicitud obtenido: {IdSolicitud}", idSolicitud);

            // 7. Obtener entidades relacionadas
            var solicitud = await _dbContext.OT_Solicitud
                .SingleOrDefaultAsync(r => r.IdSolicitud == idSolicitud);

            if (solicitud == null)
            {
                _logger.LogError("Solicitud {IdSolicitud} no encontrada", idSolicitud);
                throw new Exception($"Solicitud {idSolicitud} no encontrada");
            }



            var cotizador = await _dbContext.COT_Cotizador
                .SingleOrDefaultAsync(r => r.IdCotizador == solicitud.IdCotizador);
            if (cotizador == null)
            {
                _logger.LogDebug("Cotizador no encotrado: {IdCotizador}", solicitud.IdCotizador);
                throw new Exception($"Cotizador no encotrado:{solicitud.IdCotizador} no encontrado");
            }

            var contrato = await _dbContext.Contrato
                .SingleOrDefaultAsync(r => r.IdContrato == solReestructurado.IdContrato);

            if (contrato == null)
            {
                _logger.LogError("Contrato {IdContrato} no encontrado", solReestructurado.IdContrato);
                throw new Exception($"Contrato {solReestructurado.IdContrato} no encontrado");
            }
            var planId = cotizador.IdPlan;
            var plan = planId > 0 ? await _dbContext.COT_Plan.SingleOrDefaultAsync(r => r.IdPlan == planId) : null;
            if (plan == null)
            {
                _logger.LogError("Plan IdPlan {planId} no encontrado", planId);
                throw new Exception($"Plan IdPlan: {planId} no encontrado");
            }


            var saldoInsolutoResult = await _databaseService.usp_SaldoLiquidacionAsync(idc, DateTime.Now);
            var saldoInsoluto = saldoInsolutoResult.FirstOrDefault();

            if (saldoInsoluto == null)
            {
                throw new Exception($"No se pudo calcular el Saldo liquidacion de IdContrato: {idc} ");
            }

            _logger.LogDebug("Saldo último contrato: {SaldoInsoluto}", saldoInsoluto);

            solReestructurado.SaldoInsoluto = saldoInsoluto.Saldo + sumaSaldoInsoluto;
            solReestructurado.CapitalNew = (solReestructurado.CapitalNew) + sumaNewCapital;

            _dbContext.RC_Reestructuracion.Update(solReestructurado);

            _logger.LogDebug("📝 Reestructuración actualizada: SaldoTotal={SaldoTotal}, CapitalNew={CapitalNew}",
                solReestructurado.SaldoInsoluto, solReestructurado.CapitalNew);

            _logger.LogDebug("Actualizando cotizador ID: {IdCotizador}", cotizador.IdCotizador);

            // Calcular monto seguro (copia exacta)

            var maxPorcentaje = await _dbContext.COT_PlanComision
                .Where(s => s.IdPlan == plan.IdPlan)
                .MaxAsync(w => w.IdComisionNavigation.Porcentaje);
            maxPorcentaje ??= 0.0m;
            maxPorcentaje = maxPorcentaje / 100.0m;

            var montoSeguro = contrato.SaldoInsoluto * (maxPorcentaje / 100);

            // Actualizar montos (copia exacta)
            cotizador.MontoPrestamo = (solReestructurado.SaldoInsoluto ?? 0) +
                (plan.MontoReestructura ?? 0) + (montoSeguro ?? 0);


            // Generar folio (reemplazo de GetFolio)
            cotizador.Folio = await _folioService.GenerarFolioAsync(cotizador.IdCotizador, cotizador.IdAgencia ?? 0);

            _dbContext.COT_Cotizador.Update(cotizador);

            _logger.LogDebug("📊 Cotizador actualizado: MontoPrestamo2={MontoPrestamo2}, Folio={Folio}",
                cotizador.MontoPrestamo, cotizador.Folio);

            // 12. Asignar asesor a la solicitud (copia exacta)
            solicitud.IdAsesor = idAsesor;

            _dbContext.OT_Solicitud.Update(solicitud);

            _logger.LogDebug("👤 Asesor {IdAsesor} asignado a solicitud", idAsesor);

            // 13. Commit de la transacción
            await _unitOfWork.CommitTransactionAsync();


            return Result.Success(new ReestructuracionResult(
                solicitud.IdSolicitud,
                cotizador.IdCotizador,
                cotizador.Folio)
            );

        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "❌ Error en confirmación de cotización");
            return Result.Error(ex.Message);

        }
    }

    public async Task<Result> ReestructurarUpdateSolicitd(int idSolicitud, int idCotizador, CotizadorOpcionDto opcion)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {

            var solicitud = await _dbContext.OT_Solicitud.SingleOrDefaultAsync(r => r.IdSolicitud == idSolicitud);
            //  var cotizador = await _cotizadorRepository.GetByIdAsync(idCotizador);
            var rees = await _dbContext.RC_Reestructuracion
                .Include(i => i.RC_ProcesoHistorial)
                .Where(r => r.IdSolicitudNew == idSolicitud)
                .OrderByDescending(r => r.IdReestructuracion)
                .FirstOrDefaultAsync(); ;

            if (solicitud == null) throw new Exception("NO se encontro solicitud");
            if (rees == null) throw new Exception("NO se encontro reestructura");

            solicitud.IdEstatusSolicitud = 1;
            solicitud.IdPersonaJuridica = 1;
            solicitud.FechaFirmaContrato = DateTime.Now;
            solicitud.FechaPrimeraRenta = DateTime.Now;
            solicitud.IdAsesor = CotizacionConstants.USUARIO_DEFAULT_ASESOR_REESTRUCTURA;
            solicitud.Reestructurado = true;
            solicitud.Activo = true;
            solicitud.FechaAlta = DateTime.Now;
            solicitud.EsImportada = false;

            _dbContext.OT_Solicitud.Update(solicitud);




            var plazo = await _dbContext.COT_Plazo.FirstOrDefaultAsync(r => r.Plazo == opcion.Plazo);
            if (plazo == null) throw new Exception("NO se encontro Plazo");

            var contrato = await _dbContext.Contrato.SingleOrDefaultAsync(r => r.IdContrato == rees.IdContrato);
            if (contrato == null) throw new Exception("NO se encontro contrato");

            rees.IdPlanNew = 1;
            rees.IdPlazo = plazo.IdPlazo;
            rees.PlazoNew = opcion.Plazo;
            rees.TasaNew = contrato.Tasa ?? 0;
            rees.PagoFijoTotalAproximado = opcion.DescuentoMensual;

            _dbContext.RC_Reestructuracion.Update(rees);

            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error(ex.Message);
        }

    }

    private async Task<(List<int> listaIdRees, int idSolicitud)> EjecutarSpsConCommitPropioAsync(string contratos)
    {
        // COPIA EXACTA de tu lógica de SPs
        var listaContratos = contratos.Split('_').ToList();
        var idsContratos = await _dbContext.Contrato
                .Where(c => listaContratos.Contains(c.Contrato1))
                .Select(c => c.IdContrato)
                .ToListAsync();

        var band = true;
        var listaIdRees = new List<int>();
        var esError = false;

        for (int i = 0; i < idsContratos.Count; i++)
        {
            var idContrato = idsContratos[i];

            if (i == idsContratos.Count - 1)
            {
                var resultSp = await _databaseService.usp_RC_IniciaReestructuracionIMSSAsync(idContrato);
                var result = resultSp.FirstOrDefault();
                if (result != null)
                {
                    esError = result.EsError ?? false;
                }
                break;
            }

            if (idContrato != 0)
            {
                var resultSp = await _databaseService.usp_RC_IniciaReestructuracionIMSSAsync(idContrato);
                var result = resultSp.FirstOrDefault();

                if (result == null || (result.EsError ?? false))
                {
                    band = false;
                    break;
                }

                listaIdRees.Add(result.IdReestructuraActual ?? 0);


            }
        }

        if (!band || esError)
            return (new List<int>(), 0);

        // Obtener ID de solicitud creada por el último SP
        var idc = idsContratos.LastOrDefault();
        var solReestructurado = await _dbContext.RC_Reestructuracion
                .Where(r => r.IdContrato == idc)
                .OrderByDescending(r => r.IdReestructuracion)
                .FirstOrDefaultAsync();

        return (listaIdRees, solReestructurado?.IdSolicitudNew ?? 0);
    }
}
