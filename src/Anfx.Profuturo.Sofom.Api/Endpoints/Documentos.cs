using Anfx.Profuturo.Sofom.Api.Requests.Documentos;
using Anfx.Profuturo.Sofom.Application.Features.Documentos.Commands;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Anfx.Profuturo.Sofom.Api.Endpoints;

public class Documentos : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
       .WithTags("Documentos");


        group.MapPost("/GuardarExpediente", GuardarExpediente)
         .WithName("GuardarExpediente")
         .WithSummary("Guardar Expediente")
         .WithDescription("")
         .Accepts<GuardarExpedienteRequest>("application/json")
         .Produces<int>(StatusCodes.Status201Created)
         .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
         .Produces<ProblemDetails>(StatusCodes.Status409Conflict);
    }

    public async Task<IResult> GuardarExpediente(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] GuardarExpedienteRequest model)
    {
        var command = new GuardarExpedienteCommand(
            model.Folio,
            model.IdTipoDocumento,
            model.Nombre,
            model.MIME,
            model.Documento
        );
        var result = await commandMediator.SendAsync(command);
        return result.ToMinimalApiResult();
    }

}
