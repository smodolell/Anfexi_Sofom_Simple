namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class RelPagoMovimiento
{

    public int IdPagoMovimiento { get; set; }

    public int IdPago { get; set; }

    public int IdMovimiento { get; set; }

    public DateTime? FecAplicacion { get; set; }

    public decimal? CapitalPagado { get; set; }

    public decimal? InteresPagado { get; set; }

    public decimal? IVAPagado { get; set; }

    public decimal? TotalPagado { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? FecUltimoCambio { get; set; }

    public DateTime? FecCancelacion { get; set; }

    public bool? Reaplicado { get; set; }

    public string CausaCancelacion { get; set; }

    public Movimiento IdMovimientoNavigation { get; set; }

    public Pago IdPagoNavigation { get; set; }
}