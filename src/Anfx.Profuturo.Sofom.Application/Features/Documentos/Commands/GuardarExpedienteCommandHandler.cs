using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Commands;

internal class GuardarExpedienteCommandHandler(
    IValidator<GuardarExpedienteCommand> validator,
    IGuardarExpedienteSagaFactory guardarExpedienteSagaFactory,



    IApplicationDbContext dbContext, IExpedienteService expedienteService) : ICommandHandler<GuardarExpedienteCommand, Result>
{
    private readonly IValidator<GuardarExpedienteCommand> _validator = validator;
    private readonly IGuardarExpedienteSagaFactory _guardarExpedienteSagaFactory = guardarExpedienteSagaFactory;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IExpedienteService _expedienteService = expedienteService;

    public async Task<Result> HandleAsync(GuardarExpedienteCommand message, CancellationToken cancellationToken = default)
    {

        var validateResult = await _validator.ValidateAsync(message);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        try
        {
            var context = new GuardarExpedienteContext
            {
                Folio = message.Folio,
                IdTipoDocumento = message.IdTipoDocumento,
                MIME = message.MIME,
                Nombre = message.Nombre,
                Documento = message.Documento,
            };
            var saga = _guardarExpedienteSagaFactory.GuardarExpedienteSaga();

            var sagaResult = await saga.ExecuteAsync(context, cancellationToken);


            if (sagaResult.IsSuccess)
            {
                return Result.Error(new ErrorList(sagaResult.Errors));
            }
            return Result.Success();
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }






    }
}