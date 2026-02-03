using Anfx.Profuturo.Domain.Entities;

namespace Anfx.Profuturo.Sofom.Application.Common.Interfaces;

public interface IDatabaseServices
{
    Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, CancellationToken cancellationToken = default);
    Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, CancellationToken cancellationToken = default);
    Task<List<usp_RC_IniciaReestructuracionIMSSResult>> usp_RC_IniciaReestructuracionIMSSAsync(int? idContrato, CancellationToken cancellationToken = default);
}

