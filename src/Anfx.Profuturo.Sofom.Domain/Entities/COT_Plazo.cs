

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class COT_Plazo
{

    public int IdPlazo { get; set; }

    public int Plazo { get; set; }

    public ICollection<COT_PlanComision> COT_PlanComision { get; set; } = new List<COT_PlanComision>();

    public ICollection<COT_PlanPlazo> COT_PlanPlazo { get; set; } = new List<COT_PlanPlazo>();

    public ICollection<COT_PlanTasa> COT_PlanTasa { get; set; } = new List<COT_PlanTasa>();

    public ICollection<COT_Tasa> COT_Tasa { get; set; } = new List<COT_Tasa>();

    public ICollection<RC_Reestructuracion> RC_Reestructuracion { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<COT_Comision> IdComision { get; set; } = new List<COT_Comision>();
}