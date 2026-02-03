using Anfx.Profuturo.Sofom.Api.Infrastructure;
using Anfx.Profuturo.Sofom.Api.Requests.Cotizacion;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Commands;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Queries;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Anfx.Profuturo.Sofom.Api.Endpoints;

public class Cotizacion : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
          .WithTags("Cotizacion");

        group.MapGet(SimularCotizacion)
            .WithName("SimularCotizacion")
            .WithSummary("Simular Cotizacion")
            .WithDescription("");

        group.MapPost("/Confirmar", ConfirmarCotizacion)
         .WithName("ConfirmarCotizacion")
         .WithSummary("Confirmar Cotizacion")
         .WithDescription("")
         .Accepts<ConfirmarCotizacionRequest>("application/json")
         .Produces<int>(StatusCodes.Status201Created)
         .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
         .Produces<ProblemDetails>(StatusCodes.Status409Conflict);

    }


    public async Task<IResult> SimularCotizacion(
        [FromServices] IQueryMediator queryMediator,
        [AsParameters] SimularCotizacionRequest request
    )
    {
        var query = new SimularCotizacionQuery
        {
            FechaNacimiento = request.FechaNacimiento ?? "",
            IngresosMensuales = request.IngresosMensuales,
            TipoCotizacion = request.TipoCotizacion,
            IdAgencia = 3,
            TipoPension = request.TipoPension,
            MontoPrestamo = request.MontoPrestamo,
            EsReestructura = request.EsReestructura ?? 0,
            PorcentajePensionAlimentacion = request.PorcentajePensionAlimentacion ?? 0,
            IdPlan = request.IdPlan ?? 0,
            CapacidadPagoInforme = request.CapacidadPagoInforme,
            ContratosReestructura = request.ContratosReestructura ?? "",
            MontoPensionHijos = request.MontoPensionHijos,
            SeguroGastosFunerarios = request.SeguroGastosFunerarios
        };
        var result = await queryMediator.QueryAsync(query);
        return result.ToMinimalApiResult();
    }


    public async Task<IResult> ConfirmarCotizacion(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ConfirmarCotizacionRequest request)
    {
        var model = new CotizadorOpcionDto
        {
            idPlan = request.IdPlan,
            Plazo = request.Plazo,
            MontoPrestamo = request.MontoPrestamo,
            DescuentoMensual = request.DescuentoMensual,
            PorcentajeSeguro = request.PorcentajeSeguro,

            EsReestructura = request.EsReestructura ? 1 : 0,
            ContratosReestructura = request.ContratosReestructura,
        };
        var command = new ConfirmarCotizacionCommand(model);
        var result = await commandMediator.SendAsync(command);
        return result.ToMinimalApiResult();
    }
}
