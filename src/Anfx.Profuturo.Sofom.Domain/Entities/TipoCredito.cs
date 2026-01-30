

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class TipoCredito
{

    public int IdTipoCredito { get; set; }

    public string ClaveTipoCredito { get; set; }

    public string TipoCredito1 { get; set; }

    public int? IdTipoMovimiento { get; set; }

    public bool? PermiteEnganche { get; set; }

    public bool? CausaPenaPrepago { get; set; }

    public bool? PermiteBallonPayment { get; set; }

    public bool? ManejaActivos { get; set; }

    public bool? PermiteDepositoEnGarantia { get; set; }

    public bool? PermiteOpcionDeCompra { get; set; }

    public bool? ActivosConIvaSumanCapital { get; set; }

    public bool? CalculaIvaTasaReal { get; set; }

    public bool? PermiteValorResidual { get; set; }

    public string Prefijo { get; set; }

    public string Postfijo { get; set; }

    public int? Consecutivo { get; set; }

    public int? IdTipoMovimientoEnganche { get; set; }

    public int? IdTipoMovimientoBallon { get; set; }

    public int? IdTipoMovimientoValorResidual { get; set; }

    public int? IdTipoMovimientoDeposito { get; set; }

    public int? IdTipoMovimientoOpcion { get; set; }

    public int? IdEmpresa { get; set; }

    public int EsquemaFinanciero { get; set; }

    public decimal? TasaMoraDefault { get; set; }

    public int? IdTipoMovimientoMora { get; set; }

    public int? IdTipoMovimientoGastos { get; set; }

    public decimal? FactorGastoCob { get; set; }

    public decimal? MaxMontoGastoCob { get; set; }

    public int? PeriodoGracia { get; set; }

    public int? IdTipoMovimientoCancelCheque { get; set; }

    public decimal? PorcCancelacionCheque { get; set; }

    public decimal? MontoKilometraje { get; set; }

    public int? IdTipoMonedaKm { get; set; }

    public decimal? CargoCheque { get; set; }

    public decimal? MontoPenaNoDevuelto { get; set; }

    public int? IdTipoMovimientoNoDevuelto { get; set; }

    public int? IdMonedaNoDevuelto { get; set; }

    public int? MesesDePenaNoDevuelto { get; set; }

    public decimal? FactorVentaPublico { get; set; }

    public int? IdTipoMovimientoKmEx { get; set; }

    public bool GeneraRentaPorActivo { get; set; }

    public int? activo { get; set; }

    public int? IdAreaEmite { get; set; }

    public int? id_CRLS_TipoCredito { get; set; }

    public string SUB_Tipo_CIF { get; set; }

    public bool? NoSeRechaza { get; set; }

    public bool? bCertificaCuenta { get; set; }

    public decimal? dMontoParaCertificar { get; set; }

    public string sSUB_Tipo_CIF_CERT { get; set; }

    public bool? EnvioFad { get; set; }

    public string Prefijo_Cert { get; set; }

    public string SubTipo_Cert { get; set; }

    public string Prefijo_Reemb { get; set; }

    public string Tipo_Reemb { get; set; }

    public string SubTipo_Reemb { get; set; }
    public string BUSINESS_UNIT { get; set; }

    public int? TiempoPendienteCert { get; set; }

    public ICollection<COT_Plan> COT_Plan { get; set; } = new List<COT_Plan>();

    public ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();

    public Empresa IdEmpresaNavigation { get; set; }
}