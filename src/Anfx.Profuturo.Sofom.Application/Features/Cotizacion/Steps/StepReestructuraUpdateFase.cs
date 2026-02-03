using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class StepReestructuraUpdateFase(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext
) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> CompensateAsync(ConfirmarCotizacionContext context)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context)
    {
        var solicitud = await _dbContext.OT_Solicitud
            .SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);
        if (solicitud == null)
        {
            return Result.NotFound("No existe Solicitud");
        }
        var restructura = await _dbContext.RC_Reestructuracion
            .FirstOrDefaultAsync(r => r.IdSolicitudNew == solicitud.IdSolicitud);
        if (restructura == null)
        {
            return Result.NotFound("NO existe la restructura");
        }
        var proceso = await _dbContext.RC_Proceso.FirstOrDefaultAsync(r => r.ClaveProceso == "SOL");
        if (proceso == null)
        {
            return Result.NotFound("NO existe el Proceso");
        }
        var idProceso = proceso.IdProceso;
        var idAsesor = solicitud.IdAsesor;

        var idReestructura = restructura.IdReestructuracion;

        await _unitOfWork.BeginTransactionAsync();
        try
        {

            var procesoHistorial = new RC_ProcesoHistorial
            {
                IdReestructuracion = idReestructura,
                IdProceso = idProceso,
                IdEstadoProceso = 1,
                FechaRegistro = DateTime.Now,
                FechaUltimaModificacion = DateTime.Now,
                Comentarios = "Prestamo por One Click",
                EsProcesoActual = true,
                IdUsuario = idAsesor
            };

            _dbContext.RC_ProcesoHistorial.Add(procesoHistorial);

            solicitud.IdEstatusSolicitud = 1;
            restructura.IdEstatusSolicitud = 1;

            _dbContext.OT_Solicitud.Update(solicitud);
            _dbContext.RC_Reestructuracion.Update(restructura);


            await _unitOfWork.CommitTransactionAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error(ex.Message);
        }
    }
}
