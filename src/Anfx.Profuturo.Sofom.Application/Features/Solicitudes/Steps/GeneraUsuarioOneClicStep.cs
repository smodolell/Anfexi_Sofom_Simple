using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class GeneraUsuarioOneClicStep(
    IApplicationDbContext dbContext,
    IDatabaseService databaseServices
    ) : ISagaStep<CreateSolicitudContext>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseService _databaseServices = databaseServices;

    public async Task<Result> CompensateAsync(CreateSolicitudContext context)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context)
    {
        try
        {
            var model = context.Model;
            
            var usuario = await
                _dbContext.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NumeroEmpleado == model.NumeroEmpleadoVendedor);

            if (usuario == null)
            {
                await _databaseServices.CreateOneClickUserAsync();
            }
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
        
    }
}
