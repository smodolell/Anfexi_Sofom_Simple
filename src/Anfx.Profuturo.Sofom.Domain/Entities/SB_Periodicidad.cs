

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class SB_Periodicidad
{

    public int IdPeriodicidad { get; set; }

    public string CveCortaPeriodicidad { get; set; }

    public string DescPeriodicidad { get; set; }

    public int? ParamDias { get; set; }

    public int? ParamMes { get; set; }

    public bool sDefault { get; set; }

    public bool Band { get; set; }

    public bool? PedirDiasVencimiento { get; set; }

    public int? CantidadDiasVencimiento { get; set; }

    public bool? Activo { get; set; }

    public int? NoPagosMes { get; set; }

    public int NroPagosAnio { get; set; }

    public ICollection<COT_CalendarioFechasCorte> COT_CalendarioFechasCorte { get; set; } = new List<COT_CalendarioFechasCorte>();

    public ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdPeriodicidadNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdPeriodicidadNewNavigation { get; set; } = new List<RC_Reestructuracion>();
}