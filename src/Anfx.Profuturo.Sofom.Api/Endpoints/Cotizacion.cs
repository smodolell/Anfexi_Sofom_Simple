using Anfx.Profuturo.Sofom.Api.Infrastructure;
using Anfx.Profuturo.Sofom.Api.Requests.Cotizacion;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Queries;
using Ardalis.Result.AspNetCore;
using FluentValidation;
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
            IdPlan = request.IdPlan??0,
            CapacidadPagoInforme = request.CapacidadPagoInforme,
            ContratosReestructura = request.ContratosReestructura ?? "",
            MontoPensionHijos = request.MontoPensionHijos,
            SeguroGastosFunerarios = request.SeguroGastosFunerarios
        };
        var result = await queryMediator.QueryAsync(query);
        return result.ToMinimalApiResult();
    }
}
