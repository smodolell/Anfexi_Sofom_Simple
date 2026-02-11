using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

internal class StepActualizarFaseExpediente(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
    IDatabaseService databaseService
) : ISagaStep<GuardarExpedienteContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly IDatabaseService _databaseService = databaseService;

    public async Task<Result> CompensateAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var cotizador = await _dbContext.COT_Cotizador
           .Include(i => i.OT_Solicitud)
           .SingleOrDefaultAsync(r => r.Folio.Equals(context.Folio));

            if (cotizador == null) throw new Exception("No se encontro contizacion");
            var solicitud = cotizador.OT_Solicitud.FirstOrDefault();
            if (solicitud == null) throw new Exception("No se encontro solicitud");

            var idAsesor = solicitud.IdAsesor;
            var idSolicitud = solicitud.IdSolicitud;
            var resultado = _databaseService.ConsultaExpedientesCargados(cotizador.Folio);

            if (resultado.Total_Expedientes == resultado.Expedientes_Cargados)
            {
                await GuardarFaseExpediente(idSolicitud, idAsesor, "DOC", true);
                await GuardarFaseExpediente(idSolicitud, idAsesor, "CERT", false);
                await GuardarFaseExpediente(idSolicitud, idAsesor, "MCTRL", false);
                await GuardarFaseExpediente(idSolicitud, idAsesor, "VM", false);
                await GuardarFaseExpediente(idSolicitud, idAsesor, "PLD", false);

                solicitud.IdEstatusSolicitud = 6;
                _dbContext.OT_Solicitud.Update(solicitud);
                await _dbContext.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error(ex.Message);
        }

    }


    private async Task GuardarFaseExpediente(int idsolicitud, int idAsesor, string claveFase, bool completo)
    {
        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_FaseHistoria");
        if (!consecutivo.Success) throw new Exception("No se pudo generar el consecutivo correspondiente");


        var fase = _dbContext.OT_Fase.FirstOrDefault(r => r.ClaveUnica == claveFase);

        if (fase == null) throw new Exception("Fase no encontrada");

        var faseHistoria = _dbContext.OT_FaseHistoria.FirstOrDefault(r => r.IdSolicitud == idsolicitud && r.IdFase == fase.IdFase);
        if (faseHistoria == null)
        {
            var newFaseHistoria = new OT_FaseHistoria();

            newFaseHistoria.IdFaseHistoria = consecutivo.ConsecutivoGenerado;
            newFaseHistoria.IdFase = fase.IdFase;
            newFaseHistoria.IdSolicitud = idsolicitud;
            newFaseHistoria.IdEstatusFase = completo ? 4 : 1;
            newFaseHistoria.FechaEntrada = DateTime.Now;
            newFaseHistoria.FechaUltimaModificacion = DateTime.Now;
            newFaseHistoria.Comentario = "Prestamo por One Click";
            newFaseHistoria.Activo = true;
            newFaseHistoria.IdUsuario = idAsesor;

            _dbContext.OT_FaseHistoria.Add(newFaseHistoria);

            await _unitOfWork.SaveAsync();
        }

    }


}