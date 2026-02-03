using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

public class CotizadorOpcionDto
{

    public string FolioConfirmacion { get; set; } = string.Empty;
    public int? EsReestructura { get; set; }

    /// <summary>
    /// Contrato a Restructurar
    /// </summary>
    public string ContratosReestructura { get; set; } = string.Empty;
    public int Plazo { get; set; }
    public decimal MontoPrestamo { get; set; }
    public decimal Seguro { get; set; } = 0;
    public decimal PorcentajeSeguro { get; set; }
    public decimal DescuentoMensual { get; set; }
    public decimal PrestamoMenosSeguro { get; set; }
    public decimal Tasa { get; set; }
    public decimal MontoAproximadoDeposito { get; set; }
    public int idPlan { get; set; }
    public decimal CAT { get; set; }
    public List<TablaAmortizaItemDto> TablaAmortiza { get; set; } = new List<TablaAmortizaItemDto>();
}

public class TablaAmortizaItemDto
{
    public int NoPago { get; set; }
    public DateTime FecVencimiento { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal SaldoFinal { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public DateTime FecInicial { get; set; }
    public DateTime FecFinal { get; set; }
}
