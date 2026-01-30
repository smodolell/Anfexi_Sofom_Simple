using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Commands;

public record ConfirmarCotizacionCommand(CotizadorOpcionDto model) : ICommand<Result<ConfirmarCotizacionDto>>;


internal class ConfirmarCotizacionCommandHandler : ICommandHandler<ConfirmarCotizacionCommand, Result<ConfirmarCotizacionDto>>
{
    public Task<Result<ConfirmarCotizacionDto>> HandleAsync(ConfirmarCotizacionCommand message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}