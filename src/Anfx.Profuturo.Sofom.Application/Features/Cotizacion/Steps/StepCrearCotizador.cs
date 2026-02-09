using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class StepCrearCotizador(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
    IFolioService folioService

) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly IFolioService _folioService = folioService;

    public Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {

        var opcion = context.Opcion;
        var plan = await _dbContext.COT_Plan
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.IdPlan == opcion.idPlan);
        if (plan == null) return Result.NotFound("Plan no exixte");

        var plazo = await _dbContext.COT_Plazo
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Plazo == opcion.Plazo);
        if (plazo == null) return Result.NotFound("Plazo no encotrado");


        try
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("COT_Cotizador", cancellationToken);
                if (!consecutivo.Success) throw new InvalidOperationException("Error al generar consecutivo");
                var idCotizador = consecutivo.ConsecutivoGenerado;

                int idTasa = 0;
                var fechahoy = Convert.ToDateTime(DateTime.Now.ToString("d"));
                var FechaSimulacion = DateTime.Now;

                var itemDb = new COT_Cotizador
                {
                    IdCotizador = idCotizador,
                    Folio = opcion.FolioConfirmacion,
                    IdAgencia = CotizacionConstants.ID_AGENCIA_PENSIONES,
                    IdPersonaJuridica = 1,
                    IdPlan = plan.IdPlan,
                    FechaNacimiento = DateTime.Now,
                    FechaSimulacion = FechaSimulacion,
                    DescuentosFijosMensual = opcion.DescuentoMensual,
                    SueldoDisponible = 0,
                    MontoSolicitar = opcion.MontoPrestamo,
                    FrecuenciaPago = CotizacionConstants.ID_PERIODICIDAD_MENSUAL,
                    IdPlazo = plazo.IdPlazo,
                    IdTipoCotizacion = 1,
                    IdTasa = idTasa,
                    Tasa = opcion.Tasa,
                    MontoPension = 0,
                    MontoPrestamo = opcion.MontoPrestamo,
                    MontoQDeceaPagar = 0,
                    IdTipoPension = 1,
                    PrestamoVoz = false,
                    IdTipoSeguro = plan.IdTipoSeguro,
                    SeguroGastosFunerarios = false,
                    Seguro = opcion.Seguro,
                    PorcentajeSeguro = opcion.PorcentajeSeguro,
                    CapacidadPagoInforme = opcion.DescuentoMensual,
                    NumeroPagosFijos = opcion.Plazo,
                    PagoFijoTotalAproximado = opcion.DescuentoMensual,
                    PagoFijoTotalAproximadoMensual = opcion.DescuentoMensual,
                    SuapSipre = opcion.FolioConfirmacion,
                    EsOneClick = true
                };
                itemDb.Folio = await _folioService.GenerarFolioAsync(itemDb.IdCotizador, itemDb.IdAgencia ?? 0);

                await _dbContext.COT_Cotizador.AddAsync(itemDb, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                context.IdCotizador = itemDb.IdCotizador;
                context.FolioConfirmacion = itemDb.Folio;


            });
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
            
        }



    }
}
