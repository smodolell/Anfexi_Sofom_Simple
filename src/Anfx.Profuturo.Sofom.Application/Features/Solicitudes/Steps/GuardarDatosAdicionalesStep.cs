using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Domain.Entities;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class GuardarDatosAdicionalesStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {

        var model = context.Model;
        try
        {
            await _unitOfWork.BeginTransactionAsync();


            var datosSolicitud = new URL_DatosSolicitud
            {
                Folio = context.Folio,
                AvisoPrivacidad = model.AvisoPrivacidad,
                Ubicacion = model.Ubicacion,
                PermisoCamara = model.PermisoUsoCamara,
                PermisoMicrofono = model.PermisoUsoMicrofono,
                FechaInforme = model.FechaInforme,
                ConceptoInforme = model.ConceptosInforme,
                FolioConfirmacion = model.FolioConfirmacion
            };

            await _dbContext.URL_DatosSolicitud.AddAsync(datosSolicitud);
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