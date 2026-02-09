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



        //var cotizador = await _dbContext.COT_Cotizador.FirstOrDefaultAsync(w => w.Folio == message.Folio);
        //if (cotizador == null)
        //    return Result.NotFound("Cotizador no encontrado");

        //var solicitud = await _dbContext.OT_Solicitud
        //    .Include(i => i.IdSolicitanteNavigation)
        //    .FirstOrDefaultAsync(w => w.IdCotizador == cotizador.IdCotizador);
        //if (solicitud == null)
        //    return Result.NotFound("Solicitud no encontrada");

        //if (solicitud.IdSolicitanteNavigation == null)
        //    return Result.NotFound("Solicitante no encontrada");

        //var idAgencia = cotizador.IdAgencia ?? 3;
        //var idAsesor = solicitud.IdAsesor;
        //var idSolicitante = solicitud.IdSolicitanteNavigation.IdSolicitante;
        //var idQuePersona = 2;


        //try
        //{
        //    await _expedienteService.CreateOrUpdateExpediente(
        //        idSolicitante,
        //        idQuePersona,
        //        idAsesor,
        //        idAgencia
        //    );
        //    return Result.Success();
        //}
        //catch (Exception ex)
        //{

        //    return Result.Error(ex.Message);
        //}



    }
}