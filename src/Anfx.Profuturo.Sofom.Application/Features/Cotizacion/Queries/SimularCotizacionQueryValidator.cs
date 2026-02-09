using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Queries;

public class SimularCotizacionQueryValidator : AbstractValidator<SimularCotizacionQuery>
{
    private readonly IApplicationDbContext _dbContext;

    public SimularCotizacionQueryValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;


        // 1. Fecha de nacimiento 
        RuleFor(x => x.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
            .Must(BeValidDate).WithMessage("La fecha de nacimiento no tiene un formato válido.")
            .Must(BePastDate).WithMessage("La fecha de nacimiento no puede ser mayor a la fecha actual.");

        // 2. Ingresos mensuales
        RuleFor(x => x.IngresosMensuales)
            .GreaterThan(0).WithMessage("Los ingresos mensuales deben ser mayores a 0.");

        // 3. Tipo de cotización
        RuleFor(x => x.TipoCotizacion)
            .Must(x => x == 1 || x == 3)
            .WithMessage("Tipo de cotización inválido. Solo se permiten los valores 1 o 3.");

        // 4. Monto pensión hijos
        RuleFor(x => x.MontoPensionHijos)
            .Equal(0).WithMessage("El monto de pensión hijos debe ser 0.")
            .When(x => x.MontoPensionHijos.HasValue);

        // 5. Tipo de pensión
        RuleFor(x => x.TipoPension)
            .Equal(1)
            .WithMessage("El tipo de pensión debe ser 1.");

        // 6. Seguro gastos funerarios
        RuleFor(x => x.SeguroGastosFunerarios)
            .Equal(false)
            .WithMessage("El seguro de gastos funerarios no está permitido.");

        // 7. Validación de reestructura
        RuleFor(x => x.EsReestructura)
            .Must(x => x == 0 || x == 1)
            .WithMessage("El valor de reestructura debe ser 0 o 1.");


        When(x => x.EsReestructura == 1, () =>
        {
            RuleFor(x => x.ContratosReestructura)
                .NotEmpty().WithMessage("Contratos de reestructura requeridos.")
                .MustAsync(VerificarContratoAsync)
                .WithMessage("Los contratos de reestructura no son válidos.");
        });

        // ===== VALIDACIONES DEL PLAN =====

        // 8. Plan existe y está activo (ya tenías)
        RuleFor(x => x.IdPlan)
            .GreaterThan(0).WithMessage("El ID del plan debe ser mayor a 0")
            .DependentRules(() =>
            {
                RuleFor(x => x.IdPlan)
                    .MustAsync(async (idPlan, cancellationToken) =>
                        await PlanExisteYActivoAsync(idPlan, cancellationToken))
                    .WithMessage("El plan no existe o no está activo");
            });

        // 9. Plan está vigente (ya tenías)
        RuleFor(x => x.IdPlan)
            .CustomAsync(async (idPlan, context, cancellationToken) =>
                await PlanEstaVigenteAsync(idPlan, context, cancellationToken));

        // 10. Validar edad límite
        RuleFor(x => x)
            .CustomAsync(async (model, context, cancellationToken) =>
                await ValidarEdadLimiteAsync(model, context, cancellationToken));

        // 11. Validar capacidad de pago general
        RuleFor(x => x)
            .CustomAsync(async (model, context, cancellationToken) =>
                await ValidarCapacidadPagoGeneralAsync(model, context, cancellationToken));

        // ===== VALIDACIONES ESPECÍFICAS TIPO COTIZACIÓN 1 =====

        When(x => x.TipoCotizacion == 1, () =>
        {
            // 12. Monto préstamo debe ser 0
            RuleFor(x => x.MontoPrestamo)
                .Equal(0).WithMessage("El monto de préstamo debe ser 0 para tipo de cotización 1.");

            // 13. Validaciones específicas de porcentaje y capacidad
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    await ValidarPorcentajeDescuentoAsync(model, cancellationToken))
                .WithMessage("El porcentaje de pensión alimenticia no puede ser mayor al descuento del plan")
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .MustAsync(async (model, cancellationToken) =>
                            await ValidarCapacidadPagoAsync(model, cancellationToken))
                        .WithMessage("La capacidad de pago supera el monto disponible");
                });
        });
    }

    // ===== MÉTODOS AUXILIARES =====

    private bool BeValidDate(string fecha)
    {
        return DateTime.TryParseExact(fecha, "dd/MM/yyyy", new CultureInfo("es-MX"),
            DateTimeStyles.None, out _);
    }

    private bool BePastDate(string fecha)
    {
        if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", new CultureInfo("es-MX"),
            DateTimeStyles.None, out DateTime fechaNacimiento))
        {
            return fechaNacimiento <= DateTime.Now;
        }
        return false;
    }

    private async Task<bool> PlanExisteYActivoAsync(int idPlan, CancellationToken cancellationToken)
    {
        return await _dbContext.COT_Plan
            .AnyAsync(a => a.IdPlan == idPlan && a.Activo, cancellationToken);
    }

    private async Task PlanEstaVigenteAsync(
        int idPlan,
        ValidationContext<SimularCotizacionQuery> context,
        CancellationToken cancellationToken)
    {
        var plan = await _dbContext.COT_Plan
            .SingleOrDefaultAsync(a => a.IdPlan == idPlan, cancellationToken);

        if (plan == null)
        {
            context.AddFailure("Plan no encontrado");
            return;
        }

        if (!plan.EstaVigente())
        {
            context.AddFailure("El plan no está vigente");
        }
    }

    private async Task<bool> VerificarContratoAsync(string contratos, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(contratos))
            return false;
        var contratosSplit = contratos.Split('_').ToList();

        var contrato = _dbContext.Contrato
               .Where(r => contratosSplit.Contains(r.Contrato1))
               .OrderByDescending(r => r.FecActivacion)
               .FirstOrDefault();

        if (contrato == null) return false;

        return !string.IsNullOrWhiteSpace(contratos);
    }

    private async Task ValidarEdadLimiteAsync(
        SimularCotizacionQuery model,
        ValidationContext<SimularCotizacionQuery> context,
        CancellationToken cancellationToken)
    {
        if (!DateTime.TryParseExact(model.FechaNacimiento, "dd/MM/yyyy",
            new CultureInfo("es-MX"), DateTimeStyles.None, out DateTime fechaNacimiento))
        {
            return;
        }

        var plan = await _dbContext.COT_Plan
            .SingleOrDefaultAsync(p => p.IdPlan == model.IdPlan, cancellationToken);

        if (plan == null) return;

        // Calcular edad (forma más precisa)
        var edadSolicitante = CalcularEdad(fechaNacimiento);

        if (edadSolicitante > plan.EdadMaxima)
        {
            var mensaje = $"La edad supera el límite permitido de {plan.EdadMaxima} años";
            context.AddFailure(mensaje);
        }
    }

    private async Task ValidarCapacidadPagoGeneralAsync(
        SimularCotizacionQuery model,
        ValidationContext<SimularCotizacionQuery> context,
        CancellationToken cancellationToken)
    {
        var plan = await _dbContext.COT_Plan
            .SingleOrDefaultAsync(p => p.IdPlan == model.IdPlan, cancellationToken);

        if (plan == null) return;

        var montoDescuento = model.IngresosMensuales * ((plan.porcentajeDescuento ?? 0) / 100.00m);

        if (model.CapacidadPagoInforme > montoDescuento)
        {
            var mensaje = $"La capacidad de pago no puede exceder el {plan.porcentajeDescuento}% de descuento";
            context.AddFailure(mensaje);
        }
    }

    private async Task<bool> ValidarPorcentajeDescuentoAsync(
        SimularCotizacionQuery model,
        CancellationToken cancellationToken)
    {
        var plan = await _dbContext.COT_Plan
            .SingleOrDefaultAsync(r => r.IdPlan == model.IdPlan, cancellationToken);

        if (plan == null) return true;

        return (plan.porcentajeDescuento ?? 0) > model.PorcentajePensionAlimentacion;
    }

    private async Task<bool> ValidarCapacidadPagoAsync(
        SimularCotizacionQuery model,
        CancellationToken cancellationToken)
    {
        var plan = await _dbContext.COT_Plan
            .SingleOrDefaultAsync(r => r.IdPlan == model.IdPlan, cancellationToken);

        if (plan == null) return true;

        var montoFinal = CalcularMontoFinalAFP(plan, model);
        return model.CapacidadPagoInforme <= montoFinal;
    }

    private decimal CalcularMontoFinalAFP(COT_Plan plan, SimularCotizacionQuery model)
    {
        var montoCalculadoAFX = model.IngresosMensuales * ((plan.porcentajeDescuento ?? 0) / 100.00m);
        var montoPACalculadoAFX = model.PorcentajePensionAlimentacion > 0
            ? model.IngresosMensuales * (model.PorcentajePensionAlimentacion / 100.00m)
            : 0;
        var montoFinalCalculadoAFX = montoCalculadoAFX - montoPACalculadoAFX;
        return montoFinalCalculadoAFX < 0 ? 0 : montoFinalCalculadoAFX;
    }

    private int CalcularEdad(DateTime fechaNacimiento)
    {
        var hoy = DateTime.Today;
        var edad = hoy.Year - fechaNacimiento.Year;

        // Restar un año si todavía no ha llegado el cumpleaños este año
        if (fechaNacimiento.Date > hoy.AddYears(-edad))
        {
            edad--;
        }

        return edad;
    }
}