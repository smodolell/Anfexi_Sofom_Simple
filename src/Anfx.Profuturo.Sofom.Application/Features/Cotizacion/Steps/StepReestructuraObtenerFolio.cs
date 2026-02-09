using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class StepReestructuraObtenerFolio(IApplicationDbContext dbContext) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        var cotizador = await _dbContext.COT_Cotizador.SingleOrDefaultAsync(r => r.IdCotizador == context.IdCotizador);
        if (cotizador == null) return Result.NotFound("Contizador no encontrado");
        context.FolioConfirmacion = cotizador.Folio;
        return Result.Success();
    }
}
