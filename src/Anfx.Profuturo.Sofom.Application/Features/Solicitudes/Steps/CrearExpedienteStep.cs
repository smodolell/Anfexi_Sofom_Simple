using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;
using Microsoft.EntityFrameworkCore;

public class CrearExpedienteStep(
    IApplicationDbContext dbContext,
    IExpedienteService expedienteService
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IExpedienteService _expedienteService = expedienteService;

    public Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        // Lógica de compensación si es necesario
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        try
        {

            var solicitud = await _dbContext.OT_Solicitud
                .Include(i => i.Solicitante)
                .Include(i => i.IdCotizadorNavigation)
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);

            if (solicitud == null)
                return Result.NotFound("No existe solicitud");

            var solictante = solicitud.Solicitante;
            if (solictante == null)
                return Result.NotFound("No existe solicitante");

            var cotizador = solicitud.IdCotizadorNavigation;
            if (cotizador == null)
                return Result.NotFound("No existe cotizador");

            var idAgencia = cotizador.IdAgencia ?? 3;
            var idAsesor = solicitud.IdAsesor;
            var idSolicitante = solicitud.Solicitante.IdSolicitante;
            var idQuePersona = 2;


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
            return Result.Error($"Error al crear expediente: {ex.Message}");
        }
    }



   

}