namespace Anfx.Profuturo.Sofom.Application.Common.Saga;

public interface ISagaStep<TContext>
{
    Task<Result> ExecuteAsync(TContext context);
    Task<Result> CompensateAsync(TContext context);
}
