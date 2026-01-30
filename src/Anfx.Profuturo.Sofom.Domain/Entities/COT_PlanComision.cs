namespace Anfx.Profuturo.Sofom.Domain.Entities;
public class COT_PlanComision
{

    public int IdPlan { get; set; }

    public int IdPlazo { get; set; }

    public int IdComision { get; set; }

    public decimal VariacionMinima { get; set; }

    public decimal VariacionMaxima { get; set; }

    public bool Activo { get; set; }

    public COT_Comision IdComisionNavigation { get; set; }

    public COT_Plan IdPlanNavigation { get; set; }

    public COT_Plazo IdPlazoNavigation { get; set; }
}