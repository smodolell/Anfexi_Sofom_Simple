using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

internal class StepActualizarFaseExpediente(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService
) : ISagaStep<GuardarExpedienteContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public async Task<Result> CompensateAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        var cotizador = await _dbContext.COT_Cotizador
            .Include(i => i.OT_Solicitud)
            .SingleOrDefaultAsync(r => r.Folio.Equals(context.Folio));

        if (cotizador == null) return Result.Error("No se encontro contizacion");
        var solicitud = cotizador.OT_Solicitud.FirstOrDefault();
        if (solicitud == null) return Result.Error("No se encontro solicitud");

        var idAsesor = solicitud.IdAsesor;






        return await Task.FromResult(Result.Success());
    }


    private async Task GuardarFaseExpediente(int idsolicitud, int idAsesor,  string claveFase, bool completo)
    {
        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_FaseHistoria");
        if (!consecutivo.Success) throw new Exception("No se pudo generar el consecutivo correspondiente");


        var fase = _dbContext.OT_Fase.FirstOrDefault(r => r.ClaveUnica == claveFase);

        if(fase == null) throw new Exception("Fase no encontrada");

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

           await  _unitOfWork.SaveAsync();
        }

    }


}