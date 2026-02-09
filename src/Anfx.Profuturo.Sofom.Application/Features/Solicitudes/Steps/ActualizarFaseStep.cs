using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class ActualizarFaseStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService
    ) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {

        var solicitud = await _dbContext
            .OT_Solicitud
            .SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);
        if (solicitud == null) return Result.NotFound("Solicitud no encontrada");
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await GuardarFase(solicitud, "SOL", true);
            await GuardarFase(solicitud, "DOC", false);

            await _unitOfWork.CommitTransactionAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.CriticalError(ex.Message);
        }
    }

    private async Task GuardarFase(OT_Solicitud solicitud, string claveFase, bool completo)
    {
        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_FaseHistoria");
        if (!consecutivo.Success) throw new Exception("No se pudo generar el registro de OT_FaseHistoria");

        var fase = await _dbContext.OT_Fase.FirstOrDefaultAsync(r => r.ClaveUnica == claveFase);
        if (fase == null) throw new Exception("No se pudo encontra la fase");


        var faseHistoria = _dbContext.OT_FaseHistoria
            .FirstOrDefault(r => r.IdSolicitud == solicitud.IdSolicitud && r.IdFase == fase.IdFase);

        if (faseHistoria != null)
        {
            faseHistoria.IdEstatusFase = 4;
            faseHistoria.FechaUltimaModificacion = DateTime.Now;

            _dbContext.OT_FaseHistoria.Update(faseHistoria);
        }
        else
        {
            var newFaseHistoria = new OT_FaseHistoria();

            newFaseHistoria.IdFaseHistoria = consecutivo.ConsecutivoGenerado;
            newFaseHistoria.IdFase = fase.IdFase;
            newFaseHistoria.IdSolicitud = solicitud.IdSolicitud;
            newFaseHistoria.IdEstatusFase = completo ? 4 : 1;
            newFaseHistoria.FechaEntrada = DateTime.Now;
            newFaseHistoria.FechaUltimaModificacion = DateTime.Now;
            newFaseHistoria.Comentario = "Prestamo por One Click";
            newFaseHistoria.Activo = true;
            newFaseHistoria.IdUsuario = solicitud.IdAsesor;
            await _dbContext.OT_FaseHistoria.AddAsync(newFaseHistoria);
        }

    }
}
