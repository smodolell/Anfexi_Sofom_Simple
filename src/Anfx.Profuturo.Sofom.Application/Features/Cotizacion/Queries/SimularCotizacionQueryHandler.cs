using Anfx.Profuturo.Sofom.Application.Common.Dtos;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Coordinators;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Queries;

public class SimularCotizacionQueryHandler(
    IApplicationDbContext dbContext,
    CotizacionCoordinator _coordinator,
    IValidator<SimularCotizacionQuery> validator
) : IQueryHandler<SimularCotizacionQuery, Result<CotizacionDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly CotizacionCoordinator _coordinator = _coordinator;
    private readonly IValidator<SimularCotizacionQuery> _validator = validator;

    public async Task<Result<CotizacionDto>> HandleAsync(SimularCotizacionQuery message, CancellationToken cancellationToken = default)
    {


        if (message.IdPlan == 0)
        {
            message.IdPlan = GetIdPlanDefault();
        }


        var validateResult = await _validator.ValidateAsync(message);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        var model = new CotizacionDto
        {

            TipoCotizacion = message.TipoCotizacion,
            FechaNacimiento = message.FechaNacimiento,
            IngresosMensuales = message.IngresosMensuales,
            MontoPensionHijos = message.MontoPensionHijos ?? 0,
            TipoPension = message.TipoPension,
            CapacidadPagoInforme = message.CapacidadPagoInforme,
            MontoPrestamo = message.MontoPrestamo,
            SeguroGastosFunerarios = message.SeguroGastosFunerarios,
            EsReestructura = message.EsReestructura,
            ContratosReestructura = message.ContratosReestructura,
            PorcentajePensionAlimentacion = message.PorcentajePensionAlimentacion,
            IdPlan = message.IdPlan,
        };

        try
        {


            var planPazos = await _dbContext.COT_PlanPlazo
                .Where(r => r.IdPlan == message.IdPlan)
                .ToListAsync();

            model.Opciones = new List<CotizadorOpcionDto>();

            foreach (var planPlazo in planPazos)
            {

                var opcion = await CalcularOpcionParaPlazo(message, planPlazo);
                // Si la opción es válida, agregarla
                if (opcion.IsSuccess)
                {
                    model.Opciones.Add(opcion.Value);
                }
                else
                {
                    // Agregar errores al modelo (opcional, según tu lógica)
                    model.Errores.AddRange(opcion.ValidationErrors.Select(s => new ErrorDto(s.Identifier, s.ErrorMessage)));
                }
                model.Opciones.Add(opcion);
            }
            return Result.Success(model);
        }
        catch (Exception e)
        {
            return Result.CriticalError(e.Message);
        }



    }

    private async Task<Result<CotizadorOpcionDto>> CalcularOpcionParaPlazo(SimularCotizacionQuery query, COT_PlanPlazo planPlazo)
    {


        var context = new CalculoContextDto
        {
            TipoCotizacion = query.TipoCotizacion,
            IdAgencia = query.IdAgencia,
            IdPlan = planPlazo.IdPlan,
            IdPlazo = planPlazo.IdPlazo,
            IdPension = query.TipoPension,
            MontoPrestamo = query.MontoPrestamo,
            MontoPensionHijos = query.MontoPensionHijos??0,
            CapacidadPagoInforme = query.CapacidadPagoInforme,
            FechaNacimiento = query.FechaNacimiento,
            EsReestructura = query.EsReestructura == 1
        };


        return await _coordinator.CalcularOpcion(context);


    }

    private int GetIdPlanDefault()
    {
        var result = _dbContext.Database.SqlQueryRaw<int>(
                $"SELECT TOP(1) cp.IdPlan AS Value FROM COT_Plan AS cp " +
                $"INNER JOIN COT_AgenciaPlan AS cap2 ON cap2.IdPlan = cp.IdPlan " +
                $"INNER JOIN COT_Agencia AS ca ON ca.IdAgencia = cap2.IdAgencia " +
                $"WHERE cp.Activo = 1 AND ca.Nombre = 'PENSIONES' AND cp.IdTipoCredito = 8" +
                $"ORDER BY cap2.IdPlan DESC"
            ).FirstOrDefault();

        return result;
    }
}