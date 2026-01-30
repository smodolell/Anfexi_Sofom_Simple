

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Movimiento
{

    public int IdMovimiento { get; set; }

    public int IdTipoMovimiento { get; set; }

    public int? IdPersona { get; set; }

    public string Descripcion { get; set; }

    public int NoPago { get; set; }

    public DateTime? FecMovimiento { get; set; }

    public decimal? Capital { get; set; }

    public decimal? Interes { get; set; }

    public decimal? IVA { get; set; }

    public decimal? Total { get; set; }

    public decimal? SaldoCapital { get; set; }

    public decimal? SaldoInteres { get; set; }

    public decimal? SaldoIVA { get; set; }

    public decimal? SaldoTotal { get; set; }

    public DateTime? FecUltimoCambio { get; set; }

    public int? IdContrato { get; set; }

    public Contrato IdContratoNavigation { get; set; }

    public ICollection<RelPagoMovimiento> RelPagoMovimiento { get; set; } = new List<RelPagoMovimiento>();
}