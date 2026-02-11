using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Configuracion.Commands;

public class UpdateConfiguracionCommandValidator : AbstractValidator<UpdateConfiguracionCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateConfiguracionCommandValidator(IApplicationDbContext dbContext)
    {

        _dbContext = dbContext;

        RuleFor(x => x.MontoMinimo)
         .GreaterThan(0)
            .WithMessage("El monto mínimo debe ser mayor a 0")
         .PrecisionScale(2, 18, false)
            .WithMessage("El monto debe tener máximo 2 decimales")
         .LessThanOrEqualTo(9999999999999999.99m)
            .WithMessage("El monto excede el límite permitido");


        RuleFor(x => x.PorcentajeActual)
            .InclusiveBetween(0, 100)
            .WithMessage("El porcentaje actual debe estar entre 0 y 100");

        RuleFor(x => x.TipoPensionId)
            .MustAsync(ValidarTipoPensionIdAsync)
            .WithMessage("El tipo Pension no es válido");

        RuleFor(x => x.TipoPrestamoId)
            .MustAsync(ValidarTipoPrestamoIdAsync)
            .WithMessage("El tipo prestamo no es válido");


        RuleFor(x => x.TiempoActual)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El tiempo actual no puede ser negativo")
            .WithErrorCode("CONFIG_010")
            .LessThanOrEqualTo(999) // Límite razonable (meses/años)
            .WithMessage("El tiempo actual excede el límite permitido");

        RuleFor(x => x.PorcSelfieVSINE)
            .InclusiveBetween(0, 100)
            .WithMessage("El porcentaje Selfie vs SINE debe estar entre 0 y 100")
            .WithErrorCode("CONFIG_012")
            .PrecisionScale(2, 5, false) // 2 decimales, 5 dígitos totales (100.00)
            .WithMessage("El porcentaje debe tener máximo 2 decimales");

    }
    private async Task<bool> ValidarTipoPensionIdAsync(int tipoPensionId, CancellationToken cancellationToken)
    {
        if (tipoPensionId == 0) return false;
        var tipoPension = await _dbContext.TipoPensiones.FirstOrDefaultAsync(r => r.Id == tipoPensionId);
        return tipoPension != null;
    }

    private async Task<bool> ValidarTipoPrestamoIdAsync(int tipoPerstamoId, CancellationToken cancellationToken)
    {
        if (tipoPerstamoId == 0) return false;
        var tipoPrestamo = await _dbContext.TipoPrestamos.FirstOrDefaultAsync(r => r.Id == tipoPerstamoId);
        return tipoPrestamo != null;
    }
}
