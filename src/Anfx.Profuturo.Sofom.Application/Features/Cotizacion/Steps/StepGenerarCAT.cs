using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

class StepGenerarCAT(ICotizadorService cotizadorService) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly ICotizadorService _cotizadorService = cotizadorService;

    public Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }
    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        var result = await _cotizadorService.CalcularCAT(context.IdCotizador);

        return result;
    }
}