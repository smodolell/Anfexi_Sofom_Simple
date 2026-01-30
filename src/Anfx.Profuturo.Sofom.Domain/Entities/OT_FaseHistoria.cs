

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_FaseHistoria
{

    public int IdFaseHistoria { get; set; }

    public int IdFase { get; set; }

    public int IdSolicitud { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstatusFase { get; set; }

    public DateTime? FechaEntrada { get; set; }

    public DateTime? FechaUltimaModificacion { get; set; }

    public string Comentario { get; set; }

    public bool Activo { get; set; }

    public OT_EstatusFase IdEstatusFaseNavigation { get; set; }

    public OT_Solicitud IdSolicitudNavigation { get; set; }

    public Usuario IdUsuarioNavigation { get; set; }
}