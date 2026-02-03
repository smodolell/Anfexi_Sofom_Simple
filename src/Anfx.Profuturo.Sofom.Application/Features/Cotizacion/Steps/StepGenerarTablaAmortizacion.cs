using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

class StepGenerarTablaAmortizacion(
    IApplicationDbContext dbContext,
    ITablaAmortizacionService tablaAmortizacionService
) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ITablaAmortizacionService _tablaAmortizacionService = tablaAmortizacionService;

    public Task<Result> CompensateAsync(ConfirmarCotizacionContext context)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context)
    {
        var cotizador = await _dbContext.COT_Cotizador.SingleOrDefaultAsync(r => r.IdCotizador == context.IdCotizador);
        if (cotizador == null) return Result.NotFound("No se encontro el cotizador");



        var tablaAmotiza = await _tablaAmortizacionService.GenerarTablaAmortizacionAsync(
            cotizador.IdPlan,
            cotizador.IdPlazo,
            cotizador.IdAgencia ?? 2,
            CotizacionConstants.ID_PERIODICIDAD_MENSUAL,
            cotizador.MontoSolicitar
        );

        var result = await _tablaAmortizacionService.GuardarTablaAmortizacionAsync(cotizador.IdCotizador,tablaAmotiza);

        if (result.IsSuccess)
        {
            return Result.SuccessWithMessage("Cotizador Creado");
        }
        return Result.Error(new ErrorList(result.Errors));
    }
}
