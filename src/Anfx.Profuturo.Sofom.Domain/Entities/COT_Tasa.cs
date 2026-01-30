

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class COT_Tasa
{

    public int IdTasa { get; set; }

    public int IdTipoTasa { get; set; }

    public decimal? Valor { get; set; }

    public int? IdPlazo { get; set; }

    public string Nombre { get; set; }

    public decimal? PuntajeMaximo { get; set; }

    public decimal? PuntajeMinimo { get; set; }

    public ICollection<COT_PlanTasa> COT_PlanTasa { get; set; } = new List<COT_PlanTasa>();

    public COT_Plazo IdPlazoNavigation { get; set; }
}