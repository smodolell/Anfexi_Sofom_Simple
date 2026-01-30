

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Pago
{

    public int IdPago { get; set; }

    public int IdTipoPago { get; set; }

    public int IdCuentaBancaria { get; set; }

    public int? IdPersona { get; set; }

    public string Contrato { get; set; }

    public DateTime? FecPagoRegistro { get; set; }

    public DateTime? FecPagoValor { get; set; }

    public decimal? MontoPago { get; set; }

    public decimal? MontoPagoAplicado { get; set; }

    public decimal? SaldoPago { get; set; }

    public bool? Suspenso { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? FecUltimoCambio { get; set; }

    public string ReferenciaNumerica { get; set; }

    public ICollection<RelPagoMovimiento> RelPagoMovimiento { get; set; } = new List<RelPagoMovimiento>();
}