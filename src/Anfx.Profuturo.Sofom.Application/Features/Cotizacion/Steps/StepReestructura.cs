using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Contratos.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class StepReestructura(IReestructuracionService reestructuraService) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IReestructuracionService _reestructuraService = reestructuraService;

    public Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        var op = context.Opcion;
        var result = await _reestructuraService.IniciarReestructuracion(
            op.ContratosReestructura,
            CotizacionConstants.USUARIO_DEFAULT_ASESOR
        );
        if (result.IsSuccess)
        {
            context.IdCotizador = result.Value.IdCotizador;
            context.IdSolicitud = result.Value.IdSolicitud;
            return Result.SuccessWithMessage("StepReestructurarIMSS OK");
        }
        return Result.Error(new ErrorList(result.Errors));

    }
}
