using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Configuracion.Commands;

public class UpdateConfiguracionCommandHandler(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IValidator<UpdateConfiguracionCommand> validator
    )


: ICommandHandler<UpdateConfiguracionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IValidator<UpdateConfiguracionCommand> _validator = validator;

    public async Task<Result> HandleAsync(UpdateConfiguracionCommand message, CancellationToken cancellationToken = default)
    {
        var validateResult = await _validator.ValidateAsync(message);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        var configuracion = await _dbContext.Configuraciones.FirstOrDefaultAsync();

        if (configuracion == null)
        {
            configuracion = new Domain.Entities.Configuracion();
            _dbContext.Configuraciones.Add(configuracion);

        }

        configuracion.MontoMinimo = message.MontoMinimo;
        configuracion.PorcentajeActual = message.PorcentajeActual;
        configuracion.TiempoActual = message.TiempoActual;
        configuracion.PorcSelfieVSINE = message.PorcSelfieVSINE;
        configuracion.TipoPensionId = message.TipoPensionId;
        configuracion.TipoPrestamoId = message.TipoPrestamoId;

        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }



}