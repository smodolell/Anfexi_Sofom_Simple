namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class COT_Comision
{
    public int IdComision { get; set; }

    public string Comision { get; set; }

    public decimal? Porcentaje { get; set; }

    public int? IdTipoSeguro { get; set; }

    public ICollection<COT_PlanComision> COT_PlanComision { get; set; } = new List<COT_PlanComision>();

    public ICollection<COT_Plazo> IdPlazo { get; set; } = new List<COT_Plazo>();
}