using System.Collections.Concurrent;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Anfx.Profuturo.Sofom.Application.Common.Saga;


//public class SagaOrchestrator<TContext>
//{
//    private readonly List<ISagaStep<TContext>> _steps = new();

//    public SagaOrchestrator<TContext> AddStep(ISagaStep<TContext> step)
//    {
//        _steps.Add(step);
//        return this;
//    }

//    public async Task<Result> ExecuteAsync(TContext context)
//    {
//        var executedSteps = new Stack<ISagaStep<TContext>>();

//        foreach (var step in _steps)
//        {
//            var result = await step.ExecuteAsync(context);
//            if (!result.IsSuccess)
//            {
//                // rollback en orden inverso
//                while (executedSteps.Count > 0)
//                {
//                    var prevStep = executedSteps.Pop();
//                    await prevStep.CompensateAsync(context);
//                }
//                return result;
//            }
//            executedSteps.Push(step);
//        }

//        return Result.Success();
//    }
//}



public class SagaOrchestrator<TContext> : ISagaOrchestrator<TContext>
{
    private readonly List<ISagaStep<TContext>> _steps = new();
    private readonly ConcurrentBag<SagaStepExecution> _executionHistory = new();
    private readonly ILogger<SagaOrchestrator<TContext>> _logger;

    public SagaOrchestrator(ILogger<SagaOrchestrator<TContext>> logger = null)
    {
        _logger = logger;
    }

    public void AddStep(ISagaStep<TContext> step)
    {
        _steps.Add(step);
        _logger?.LogDebug("Step agregado: {StepName}", GetStepName(step));
    }

    public async Task<Result> ExecuteAsync(TContext context, CancellationToken cancellationToken = default)
    {
        _logger?.LogInformation("Iniciando ejecución de saga con {StepCount} steps", _steps.Count);

        var executedSteps = new Stack<ISagaStep<TContext>>();
        var compensationResults = new List<Result>();

        foreach (var step in _steps)
        {
            var stepName = GetStepName(step);
            var execution = new SagaStepExecution
            {
                StepName = stepName,
                StartTime = DateTime.Now
            };

            try
            {
                _logger?.LogDebug("Ejecutando step: {StepName}", stepName);

                var result = await step.ExecuteAsync(context,cancellationToken);
                execution.EndTime = DateTime.Now;
                execution.IsSuccess = result.IsSuccess;

                if (result.IsSuccess)
                {
                    _logger?.LogDebug("Step exitoso: {StepName} ({Duration}ms)",
                        stepName, execution.Duration.TotalMilliseconds);
                    executedSteps.Push(step);
                }
                else
                {
                    execution.ErrorMessage = string.Join(",", result.Errors);
                    _logger?.LogError("Step fallido: {StepName} - {Error}", stepName, string.Join(",", result.Errors));

                    // Iniciar compensación
                    await CompensateExecutedSteps(executedSteps, context, compensationResults);

                    _executionHistory.Add(execution);
                    return Result.Error($"Error en step '{stepName}': {string.Join(",", result.Errors)}");
                }
            }
            catch (Exception ex)
            {
                execution.EndTime = DateTime.Now;
                execution.IsSuccess = false;
                execution.ErrorMessage = ex.Message;

                _logger?.LogError(ex, "Excepción en step: {StepName}", stepName);

                // Iniciar compensación
                await CompensateExecutedSteps(executedSteps, context, compensationResults);

                _executionHistory.Add(execution);
                return Result.Error($"Excepción en step '{stepName}': {ex.Message}");
            }

            _executionHistory.Add(execution);
        }

        _logger?.LogInformation("Saga completada exitosamente");
        return Result.Success();
    }

    private async Task CompensateExecutedSteps(
        Stack<ISagaStep<TContext>> executedSteps,
        TContext context,
        List<Result> compensationResults)
    {
        _logger?.LogWarning("Iniciando compensación de {StepCount} steps ejecutados",
            executedSteps.Count);

        while (executedSteps.Count > 0)
        {
            var step = executedSteps.Pop();
            var stepName = GetStepName(step);

            try
            {
                _logger?.LogDebug("Compensando step: {StepName}", stepName);
                var result = await step.CompensateAsync(context);

                if (result.IsSuccess)
                {
                    _logger?.LogDebug("Compensación exitosa: {StepName}", stepName);
                }
                else
                {
                    _logger?.LogError("Compensación fallida: {StepName} - {Error}",
                        stepName, result.Errors);
                }

                compensationResults.Add(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Excepción durante compensación de step: {StepName}", stepName);
                compensationResults.Add(Result.Error($"Excepción: {ex.Message}"));
            }
        }

        LogCompensationSummary(compensationResults);
    }

    private void LogCompensationSummary(List<Result> compensationResults)
    {
        var successful = compensationResults.Count(r => r.IsSuccess);
        var failed = compensationResults.Count(r => !r.IsSuccess);

        _logger?.LogInformation("Resumen de compensación: {Successful} exitosas, {Failed} fallidas",
            successful, failed);
    }

    public List<SagaStepExecution> GetExecutionHistory()
    {
        return _executionHistory.ToList();
    }

    private string GetStepName(ISagaStep<TContext> step)
    {
        return step.GetType().Name.Replace("Step", "");
    }
}
