using Anfx.Profuturo.Sofom.Api.Infrastructure;
using Anfx.Profuturo.Sofom.Api.Requests.Cotizacion;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Commands;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.DTOs;
using Ardalis.Result.AspNetCore;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Anfx.Profuturo.Sofom.Api.Endpoints;

public class Solicitudes : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Solicitudes");

        group.MapPost("/Crear", CreateSolicitud)
            .WithName("CreateSolicitud")
            .WithSummary("Crear Solicitud")
            .WithDescription("")
            .Accepts<SolicitudModel>("application/json")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict);

    }

    public async Task<IResult> CreateSolicitud(
      [FromServices] ICommandMediator commandMediator,
      [FromBody] SolicitudModel model)
    {
        var command = new CreateSolicitudCommand(model);
        var result = await commandMediator.SendAsync(command);
        return result.ToMinimalApiResult();
    }


}