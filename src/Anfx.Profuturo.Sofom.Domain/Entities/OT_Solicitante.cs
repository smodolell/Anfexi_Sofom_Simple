

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_Solicitante
{

    public int IdSolicitante { get; set; }

    public int IdPersonaJuridica { get; set; }

    public string Nombres_RazonSocial { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string RFC { get; set; }

    public string Email { get; set; }

    public ICollection<OT_SolicitanteDireccion> OT_SolicitanteDireccion { get; set; } = new List<OT_SolicitanteDireccion>();

    public OT_SolicitantePF OT_SolicitantePF { get; set; }

    public ICollection<OT_SolicitantePFCuentaBancaria> OT_SolicitantePFCuentaBancaria { get; set; } = new List<OT_SolicitantePFCuentaBancaria>();

    public ICollection<OT_Solicitante_InfoPension> OT_Solicitante_InfoPension { get; set; } = new List<OT_Solicitante_InfoPension>();

    public ICollection<OT_Solicitud> OT_Solicitud { get; set; } = new List<OT_Solicitud>();
	public ICollection<OC_ElementosAuditoria> OC_ElementosAuditoria { get; set; }
}