using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

public class CotizadorOpcionDtoValidator : AbstractValidator<CotizadorOpcionDto>
{
    private readonly IApplicationDbContext _dbContext;

    public CotizadorOpcionDtoValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.EsReestructura)
            .Must(x => !x.HasValue || x == 0 || x == 1)
            .WithMessage("Error 1000: El campo EsReestructura debe ser 0 o 1");

        RuleFor(x => x.ContratosReestructura)
            .NotEmpty()
            .When(x => x.EsReestructura == 1)
            .WithMessage("Error 1001: Se requiere el contrato a reestructurar");

  

        RuleFor(x => x)
            .MustAsync(async (dto, cancellation) =>
            {
                var plazoExiste = await _dbContext.COT_Plazo
                    .AnyAsync(p => p.Plazo == dto.Plazo, cancellation);
                if (!plazoExiste) return false;

                var planPlazoExiste = await _dbContext.COT_PlanPlazo
                    .Include(i => i.IdPlazoNavigation)
                    .AnyAsync(pp => pp.IdPlan == dto.idPlan &&
                            pp.IdPlazoNavigation.Plazo == dto.Plazo, cancellation);
                return planPlazoExiste;
            })
            .WithName("Plazo")
            .WithMessage("Error 1002: El plazo no es válido");

        RuleFor(x => x.MontoPrestamo)
            .GreaterThan(0)
                .WithMessage("El monto del préstamo debe ser mayor a cero");
            

        RuleFor(x => x.PorcentajeSeguro)
         .GreaterThan(0)
         .When(x =>
         {
             var plan = _dbContext.COT_Plan.SingleOrDefault(r => r.IdPlan == x.idPlan);
             if (plan == null) return false;
             return plan.IdTipoSeguro != 3;
         })
         .WithMessage("El porcentaje del seguro debe ser mayor a cero cuando es requerido");

        RuleFor(x => x.DescuentoMensual)
           .GreaterThan(0)
           .WithMessage("El descuento mensual debe ser mayor a cero");
    }



}
