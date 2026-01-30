

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class RC_Reestructuracion
{

    public int IdReestructuracion { get; set; }

    public int IdSolicitud { get; set; }

    public int? IdSolicitudNew { get; set; }

    public int IdContrato { get; set; }

    public int? IdContratoNew { get; set; }

    public int? IdContratoReservado { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public int? IdAgencia { get; set; }

    public decimal Capital { get; set; }

    public decimal? CapitalPagado { get; set; }

    public int Plazo { get; set; }

    public int IdPeriodicidad { get; set; }

    public int? IdPlan { get; set; }

    public int? IdPlanNew { get; set; }

    public decimal Tasa { get; set; }

    public decimal CapitalNew { get; set; }

    public int? IdPlazo { get; set; }

    public int PlazoNew { get; set; }

    public int IdPeriodicidadNew { get; set; }

    public decimal TasaNew { get; set; }

    public decimal? PagoFijoTotalAproximado { get; set; }

    public decimal? PagoFijoTotalAproximadoMensual { get; set; }

    public DateTime? FechaFirmaContrato { get; set; }

    public DateTime? FechaPrimeraRenta { get; set; }

    public bool Activo { get; set; }

    public decimal? CapitalALibrerar { get; set; }

    public bool? LiberaCapital { get; set; }

    public decimal? MontoDisponible { get; set; }

    public int IdEstatusSolicitud { get; set; }

    public int IdAsesor { get; set; }

    public decimal? SaldoInsoluto { get; set; }

    public decimal? MontoDepositoRes { get; set; }

    public bool? EsMigrada { get; set; }

    public bool EnInbox { get; set; }

    public COT_Agencia IdAgenciaNavigation { get; set; }

    public Contrato IdContratoNavigation { get; set; }

    public Contrato IdContratoNewNavigation { get; set; }

    public SB_Periodicidad IdPeriodicidadNavigation { get; set; }

    public SB_Periodicidad IdPeriodicidadNewNavigation { get; set; }

    public COT_Plan IdPlanNavigation { get; set; }

    public COT_Plan IdPlanNewNavigation { get; set; }

    public COT_Plazo IdPlazoNavigation { get; set; }

    public OT_Solicitud IdSolicitudNavigation { get; set; }

    public OT_Solicitud IdSolicitudNewNavigation { get; set; }

    public ICollection<RC_ProcesoHistorial> RC_ProcesoHistorial { get; set; } = new List<RC_ProcesoHistorial>();
}