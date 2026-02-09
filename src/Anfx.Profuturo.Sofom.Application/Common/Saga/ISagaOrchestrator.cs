namespace Anfx.Profuturo.Sofom.Application.Common.Saga;

public interface ISagaOrchestrator<TContext>
{
    void AddStep(ISagaStep<TContext> step);
    Task<Result> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
    List<SagaStepExecution> GetExecutionHistory();
}
