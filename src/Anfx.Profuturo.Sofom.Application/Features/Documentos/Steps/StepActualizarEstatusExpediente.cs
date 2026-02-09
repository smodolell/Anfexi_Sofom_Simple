using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

internal class StepActualizarEstatusExpediente(IUnitOfWork unitOfWork, IApplicationDbContext dbContext) : ISagaStep<GuardarExpedienteContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> CompensateAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {

        try
        {
            var expedienteDocumento = await _dbContext.EXP_ExpedienteDocumento
                .SingleOrDefaultAsync(r => r.IdExpedienteDocumento == context.IdExpedienteDocumento);


            if (expedienteDocumento == null)
            {
                return Result.NotFound("Expediente Documento no encontrado");
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            expedienteDocumento.IdEstadoDocumento = 1;
            expedienteDocumento.FechaUltimaModificacion = DateTime.Now;
            expedienteDocumento.Comentario = "Ingresado por One Clic";
            
            _dbContext.EXP_ExpedienteDocumento.Update(expedienteDocumento);
            
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error(ex.Message);
        }

    }
}

