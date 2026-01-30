namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class COT_PlanPlazo
{

    public int IdPlan { get; set; }

    public int IdPlazo { get; set; }

    public bool? Asignado { get; set; }

    public COT_Plan IdPlanNavigation { get; set; }

    public COT_Plazo IdPlazoNavigation { get; set; }
}