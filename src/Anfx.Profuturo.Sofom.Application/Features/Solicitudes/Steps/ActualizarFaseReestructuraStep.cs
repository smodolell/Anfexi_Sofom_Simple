using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;
using Microsoft.EntityFrameworkCore;

public class ActualizarFaseReestructuraStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    public Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        var solicitud = await _dbContext.OT_Solicitud
            .Include(i => i.IdCotizadorNavigation)
            .SingleOrDefaultAsync(s => s.IdSolicitud == context.IdSolicitud);

        if (solicitud == null) return Result.NotFound("Solicitud no existe");

        var reestructura = await _dbContext.RC_Reestructuracion
                .Include(i => i.RC_ProcesoHistorial)
                .Where(r => r.IdSolicitudNew == context.IdSolicitud)
                .OrderByDescending(r => r.IdReestructuracion)
                .FirstOrDefaultAsync();

        if (reestructura == null) return Result.NotFound("Reestructura no encontrada");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            solicitud.IdEstatusSolicitud = 35;
            reestructura.IdEstatusSolicitud = 8;

            await GuardarFaseReestructuracion(reestructura, "SOL", true, solicitud.IdAsesor);
            await GuardarFaseReestructuracion(reestructura, "DOC", false, solicitud.IdAsesor);

            _dbContext.RC_Reestructuracion.Update(reestructura);
            _dbContext.OT_Solicitud.Update(solicitud);

            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error(ex.Message);
        }
    }


    private async Task GuardarFaseReestructuracion(RC_Reestructuracion reestructura, string claveFase, bool completo, int idAsesor)
    {
        var proceso = await _dbContext.RC_Proceso.FirstOrDefaultAsync(r => r.ClaveProceso == claveFase);
        if (proceso == null) throw new Exception("Proceso no encontrado");

        var procesoHistorial = reestructura.RC_ProcesoHistorial.FirstOrDefault(r => r.IdProceso == proceso.IdProceso);

        if (procesoHistorial != null)
        {
            procesoHistorial.IdEstadoProceso = 2;
            procesoHistorial.FechaUltimaModificacion = DateTime.Now;
        }
        else
        {
            var model = new RC_ProcesoHistorial
            {
                IdReestructuracion = reestructura.IdReestructuracion,
                IdProceso = proceso.IdProceso,
                IdEstadoProceso = completo ? 2 : 1,
                FechaRegistro = DateTime.Now,
                FechaUltimaModificacion = DateTime.Now,
                Comentarios = "Prestamo por VoiceBot",
                EsProcesoActual = true,
                IdUsuario = idAsesor
            };
            reestructura.RC_ProcesoHistorial.Add(model);
        }
        
    }
}