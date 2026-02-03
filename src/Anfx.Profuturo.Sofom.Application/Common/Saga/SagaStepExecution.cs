namespace Anfx.Profuturo.Sofom.Application.Common.Saga;

public class SagaStepExecution
{
    public string StepName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public TimeSpan Duration => (EndTime ?? DateTime.Now) - StartTime;
}