namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class COT_Plan
{
    public int IdPlan { get; set; }

    public string NombrePlan { get; set; }

    public int IdTipoCredito { get; set; }

    public string Descripcion { get; set; }

    public bool Activo { get; set; }

    public bool AplicaAFlotilla { get; set; }

    public decimal RangoValorResidual { get; set; }

    public decimal RangoMantenimiento { get; set; }

    public decimal? RangoEnganche { get; set; }

    public DateTime? FechaVigenciaFin { get; set; }

    public DateTime? FechaVigenciaIni { get; set; }

    public bool? EditaDepositoGarantia { get; set; }

    public bool MantenimientoOpcional { get; set; }

    public decimal? Interes { get; set; }

    public decimal? Comision { get; set; }

    public int? IdTasa { get; set; }

    public decimal? ImporteMinimo { get; set; }

    public decimal? ImporteMaximo { get; set; }

    public decimal? MontoMinimoPension { get; set; }

    public decimal? MontoMinimoIngreso { get; set; }

    public decimal? porcentajeDescuento { get; set; }

    public decimal? MontoDescuento { get; set; }

    public int? EdadMaxima { get; set; }

    public decimal? MontoEdadMaxima { get; set; }

    public decimal? MontoReestructura { get; set; }

    public decimal? Viudez { get; set; }

    public decimal? Invalidez { get; set; }

    public decimal? ViudezOrfandad { get; set; }

    public decimal? Vejez { get; set; }

    public decimal? CesantíaEdadAvanzada { get; set; }

    public decimal? RetiroAnticipado { get; set; }

    public decimal? Tipo1 { get; set; }

    public decimal? Tipo2 { get; set; }

    public decimal? Tipo3 { get; set; }

    public decimal? Tipo4 { get; set; }

    public int EdadRechazo { get; set; }

    public decimal? PH_Viudez { get; set; }

    public decimal? PH_Invalidez { get; set; }

    public decimal? PH_ViudezOrfandad { get; set; }

    public decimal? PH_Vejez { get; set; }

    public decimal? PH_CesantiaEdadAvanzada { get; set; }

    public decimal? PH_RetiroAnticipado { get; set; }

    public decimal? PH_Tipo1 { get; set; }

    public decimal? PH_Tipo2 { get; set; }

    public decimal? PH_Tipo3 { get; set; }

    public decimal? PH_Tipo4 { get; set; }

    public string ClaveNomina { get; set; }

    public bool PrestamoVoz { get; set; }

    public int? IdTipoSeguro { get; set; }

    public ICollection<COT_Cotizador> COT_Cotizador { get; set; } = new List<COT_Cotizador>();

    public ICollection<COT_PlanComision> COT_PlanComision { get; set; } = new List<COT_PlanComision>();

    public ICollection<COT_PlanPlazo> COT_PlanPlazo { get; set; } = new List<COT_PlanPlazo>();

    public ICollection<COT_PlanTasa> COT_PlanTasa { get; set; } = new List<COT_PlanTasa>();

    public TipoCredito IdTipoCreditoNavigation { get; set; }

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdPlanNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdPlanNewNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<RangoFecha> RangoFecha { get; set; } = new List<RangoFecha>();
}