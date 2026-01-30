namespace Anfx.Profuturo.Sofom.Domain.Entities;

public  class COT_PlanTasa
{

    public int IdPlan { get; set; }

    public int IdPlazo { get; set; }

    public int IdTasa { get; set; }

    public decimal VariacionMinima { get; set; }

    public decimal VariacionMaxima { get; set; }

    public bool Activo { get; set; }

    public COT_Plan IdPlanNavigation { get; set; }

    public COT_Plazo IdPlazoNavigation { get; set; }

    public COT_Tasa IdTasaNavigation { get; set; }
}