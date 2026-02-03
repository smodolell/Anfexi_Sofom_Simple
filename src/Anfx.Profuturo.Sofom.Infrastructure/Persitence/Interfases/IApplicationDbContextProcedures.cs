using Anfx.Profuturo.Domain.Entities;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence.Extensions;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Interfases;

public partial interface IApplicationDbContextProcedures
{
    Task<List<usp_RC_IniciaReestructuracionIMSSResult>> usp_RC_IniciaReestructuracionIMSSAsync(int? idContrato, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
}