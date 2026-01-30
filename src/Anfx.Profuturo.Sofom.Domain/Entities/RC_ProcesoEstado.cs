

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class RC_ProcesoEstado
{

    public int IdEstadoProceso { get; set; }

    public string EstadoProceso { get; set; }

    public string Icono { get; set; }

    public bool PorDefecto { get; set; }

    public ICollection<RC_ProcesoHistorial> RC_ProcesoHistorial { get; set; } = new List<RC_ProcesoHistorial>();
}