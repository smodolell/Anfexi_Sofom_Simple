

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_Solicitud
{

    public int IdSolicitud { get; set; }

    public int IdEstatusSolicitud { get; set; }

    public int? IdSolicitante { get; set; }

    public int IdCotizador { get; set; }

    public int IdAsesor { get; set; }

    public DateTime? FechaAlta { get; set; }

    public decimal? PuntajeScoring { get; set; }

    public int? IdContrato { get; set; }

    public int IdPersonaJuridica { get; set; }

    public DateTime? FechaFirmaContrato { get; set; }

    public DateTime? FechaPrimeraRenta { get; set; }

    public int? IdEjecutivo { get; set; }

    public bool? EsImportada { get; set; }

    public bool? Reestructurado { get; set; }

    public decimal? PuntajeScoringBuro { get; set; }

    public decimal? PuntajeScoringCP { get; set; }

    public bool? Activo { get; set; }

    public bool? EsMigrada { get; set; }

    public ICollection<COT_SolicitudCotizador> COT_SolicitudCotizador { get; set; } = new List<COT_SolicitudCotizador>();

    public Usuario IdAsesorNavigation { get; set; }

    public Contrato IdContratoNavigation { get; set; }

    public COT_Cotizador IdCotizadorNavigation { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }

    public ICollection<OT_FaseHistoria> OT_FaseHistoria { get; set; } = new List<OT_FaseHistoria>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdSolicitudNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdSolicitudNewNavigation { get; set; } = new List<RC_Reestructuracion>();
}