using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

internal class StepCrearExpediente(IApplicationDbContext dbContext, IExpedienteService expedienteService) : ISagaStep<GuardarExpedienteContext>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IExpedienteService _expedienteService = expedienteService;

    public async Task<Result> CompensateAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        var cotizador = await _dbContext.COT_Cotizador
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Folio == context.Folio);
        if (cotizador == null)
            return Result.NotFound("Cotizador no encontrado");

        var solicitud = await _dbContext.OT_Solicitud
            .Include(i => i.IdSolicitanteNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.IdCotizador == cotizador.IdCotizador);

        if (solicitud == null)
            return Result.NotFound("Solicitud no encontrada");

        if (solicitud.IdSolicitanteNavigation == null)
            return Result.NotFound("Solicitante no encontrada");

        var idAgencia = cotizador.IdAgencia ?? 3;
        var idAsesor = solicitud.IdAsesor;
        var idSolicitante = solicitud.IdSolicitanteNavigation.IdSolicitante;
        var idQuePersona = 2;


        try
        {
            await _expedienteService.CreateOrUpdateExpediente(
                idSolicitante,
                idQuePersona,
                idAsesor,
                idAgencia
            );
            return Result.Success();
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }
    }
}

