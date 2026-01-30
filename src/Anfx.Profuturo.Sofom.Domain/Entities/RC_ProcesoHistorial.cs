

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class RC_ProcesoHistorial
{

    public int IdProcesoHistorial { get; set; }

    public int IdReestructuracion { get; set; }

    public int IdProceso { get; set; }

    public int? IdEstadoProceso { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string Comentarios { get; set; }

    public bool EsProcesoActual { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? FechaUltimaModificacion { get; set; }

    public RC_ProcesoEstado IdEstadoProcesoNavigation { get; set; }

    public RC_Reestructuracion IdReestructuracionNavigation { get; set; }
}