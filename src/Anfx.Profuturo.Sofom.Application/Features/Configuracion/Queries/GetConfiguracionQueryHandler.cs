using Anfx.Profuturo.Sofom.Application.Features.Configuracion.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Configuracion.Queries;

internal class GetConfiguracionQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetConfiguracionQuery, Result<ConfiguracionDto>>

{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<ConfiguracionDto>> HandleAsync(GetConfiguracionQuery message, CancellationToken cancellationToken = default)
    {

        var configuracion = await _dbContext.Configuraciones.FirstOrDefaultAsync();

        if (configuracion == null) return Result.NotFound("Configuracion no existe");

        var result = new ConfiguracionDto
        {
            MontoMinimo = configuracion.MontoMinimo,
            PorcentajeActual = configuracion.PorcentajeActual,
            PorcSelfieVSINE = configuracion.PorcSelfieVSINE,
            TiempoActual = configuracion.TiempoActual,
            TipoPensionId = configuracion.TipoPensionId,
            TipoPrestamoId = configuracion.TipoPrestamoId
        };

        return Result.Success(result);
    }
}