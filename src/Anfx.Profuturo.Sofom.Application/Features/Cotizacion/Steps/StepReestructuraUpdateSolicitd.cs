using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class StepReestructuraUpdateSolicitd(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext
) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {

            var solicitud = await _dbContext.OT_Solicitud.SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);
            if (solicitud == null) throw new Exception("Solicitud no existe");

            var rees = await _dbContext.RC_Reestructuracion
                .Include(i => i.RC_ProcesoHistorial)
                .Where(r => r.IdSolicitudNew == solicitud.IdSolicitud)
                .OrderByDescending(r => r.IdReestructuracion)
                .FirstOrDefaultAsync(); ;

            if (rees == null) throw new Exception("NO se encontro reestructura");

            var plazo = await _dbContext.COT_Plazo.FirstOrDefaultAsync(r => r.Plazo == context.Opcion.Plazo);
            if (plazo == null) throw new Exception("NO se encontro Plazo");

            var contrato = await _dbContext.Contrato.SingleOrDefaultAsync(r => r.IdContrato == rees.IdContrato);
            if (contrato == null) throw new Exception("NO se encontro contrato");

            solicitud.IdEstatusSolicitud = 1;
            solicitud.IdPersonaJuridica = 1;
            solicitud.FechaFirmaContrato = DateTime.Now;
            solicitud.FechaPrimeraRenta = DateTime.Now;
            solicitud.IdAsesor = CotizacionConstants.USUARIO_DEFAULT_ASESOR_REESTRUCTURA;
            solicitud.Reestructurado = true;
            solicitud.Activo = true;
            solicitud.FechaAlta = DateTime.Now;
            solicitud.EsImportada = false;

            _dbContext.OT_Solicitud.Update(solicitud);

            rees.IdPlanNew = 1;
            rees.IdPlazo = plazo.IdPlazo;
            rees.PlazoNew = context.Opcion.Plazo;
            rees.TasaNew = contrato.Tasa ?? 0;
            rees.PagoFijoTotalAproximado = context.Opcion.DescuentoMensual;

            _dbContext.RC_Reestructuracion.Update(rees);

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