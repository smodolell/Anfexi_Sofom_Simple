using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Commands;

public record ConfirmarCotizacionCommand(CotizadorOpcionDto model) : ICommand<Result<ConfirmarCotizacionDto>>;


internal class ConfirmarCotizacionCommandHandler(
    IApplicationDbContext dbContext,
    IValidator<CotizadorOpcionDto> validator,
    IConfirmarCotizacionSagaFactory confirmarCotizacionSagaFactory,
    
    ILogger<ConfirmarCotizacionCommandHandler> logger
) : ICommandHandler<ConfirmarCotizacionCommand, Result<ConfirmarCotizacionDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IValidator<CotizadorOpcionDto> _validator = validator;
    private readonly IConfirmarCotizacionSagaFactory _confirmarCotizacionSagaFactory = confirmarCotizacionSagaFactory;
    private readonly ILogger<ConfirmarCotizacionCommandHandler> _logger = logger;

    public async Task<Result<ConfirmarCotizacionDto>> HandleAsync(ConfirmarCotizacionCommand message, CancellationToken cancellationToken = default)
    {

        var model = message.model;

        var validateResult = await _validator.ValidateAsync(model);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }
        try
        {
            var context = new ConfirmarCotizacionContext
            {
                Opcion = model
            };
            var saga = _confirmarCotizacionSagaFactory.ConfirmarCotizacionSaga(model.EsReestructura == 1);

            var sagaResult = await saga.ExecuteAsync(context, cancellationToken);

            if (sagaResult.IsSuccess)
            {
                var result = new ConfirmarCotizacionDto
                {
                    folio = context.FolioConfirmacion
                };

                return Result.Success(result);
            }
            else
            {
                return Result.Error(new ErrorList(sagaResult.Errors));
            }

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error en confirmación de cotización");
            return Result.Error(ex.Message);
        }

    }


}
