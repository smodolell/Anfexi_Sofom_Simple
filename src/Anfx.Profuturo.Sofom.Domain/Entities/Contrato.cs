

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public string Contrato1 { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTipoCredito { get; set; }

    public int? IdEstatusContrato { get; set; }

    public decimal? Capital { get; set; }

    public decimal? PorcEnganche { get; set; }

    public decimal? Enganche { get; set; }

    public decimal? CapitalFinanciado { get; set; }

    public int IdPeriodicidad { get; set; }

    public int? Plazo { get; set; }

    public int IdMoneda { get; set; }

    public DateTime? FecInicioContrato { get; set; }

    public DateTime? FecPrimeraRenta { get; set; }
    public DateTime? FecActivacion { get; set; }

    public DateTime? FecFinContrato { get; set; }

    public int IdTasa { get; set; }

    public decimal? TasaBase { get; set; }

    public decimal? PuntosMas { get; set; }

    public decimal? PuntosPor { get; set; }
    public decimal? Tasa { get; set; }

    public decimal? TasaBaseMora { get; set; }

    public int IdTasaMora { get; set; }

    public decimal? PuntosMasMora { get; set; }

    public decimal? PuntosPorMora { get; set; }

    public decimal? FactorMora { get; set; }

    public decimal? TasaMora { get; set; }

    public decimal? SaldoInsoluto { get; set; }

    public decimal? BallonPayment { get; set; }

    public decimal? PorcBallonPayment { get; set; }

    public decimal? ValorResidual { get; set; }

    public decimal? PorcValorResidual { get; set; }

    public decimal? DepositoEnGarantia { get; set; }

    public decimal? OpcionDeCompra { get; set; }

    public decimal? PorcOpcionDeCompra { get; set; }

    public decimal? TasaIva { get; set; }

    public int? VersionTabla { get; set; }

    public int? IdTipoCalculoTasaVariable { get; set; }

    public decimal? NroRentasDepositoGarantia { get; set; }

    public DateTime? FechaFirmaContrato { get; set; }

    public int? IdTipoMantenimiento { get; set; }

    public int? IdContratoOriginal { get; set; }

    public int IdUnidadNegocio { get; set; }
    public decimal? CAT { get; set; }

    public string FecCierre { get; set; }

    public string MotivoEstatus { get; set; }

    public string CobranzaComercial { get; set; }

    public string ClaveContrato { get; set; }

    public int? IdSolicitud { get; set; }

    public string Folio { get; set; }

    public int? IdEstatus { get; set; }

    public string ClabeDeposito { get; set; }

    public ICollection<COT_SolicitudCotizador> COT_SolicitudCotizador { get; set; } = new List<COT_SolicitudCotizador>();

    public EstatusContrato IdEstatusContratoNavigation { get; set; }

    public SB_Periodicidad IdPeriodicidadNavigation { get; set; }

    public Persona IdPersonaNavigation { get; set; }

    public TipoCredito IdTipoCreditoNavigation { get; set; }

    public ICollection<Movimiento> Movimiento { get; set; } = new List<Movimiento>();

    public ICollection<OT_Solicitud> OT_Solicitud { get; set; } = new List<OT_Solicitud>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdContratoNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<RC_Reestructuracion> RC_ReestructuracionIdContratoNewNavigation { get; set; } = new List<RC_Reestructuracion>();

    public ICollection<TablaAmortiza> TablaAmortiza { get; set; } = new List<TablaAmortiza>();
}