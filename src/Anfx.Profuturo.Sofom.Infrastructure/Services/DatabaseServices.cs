using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence;

namespace Anfx.Profuturo.Sofom.Infrastructure.Services
{
    internal class DatabaseServices(ApplicationDbContext dbContext) : IDatabaseServices
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Procedures.usp_ProcesaSaldoInsolutoAsync(idContrato);
        }

        public async Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, CancellationToken cancellationToken = default)
        {
          return  await _dbContext.Procedures.usp_SaldoLiquidacionAsync(idContrato, fecha);
        }
    }
}
