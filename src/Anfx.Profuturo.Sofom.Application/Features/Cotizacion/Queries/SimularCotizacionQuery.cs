using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Queries;

public class SimularCotizacionQuery : IQuery<Result<CotizacionDto>>
{
    public int TipoCotizacion { get; set; }
    public int IdPlan { get; set; }
    public int IdAgencia { get; set; }
    public int TipoPension { get; set; }
    public decimal MontoPrestamo { get; set; }
    public decimal CapacidadPagoInforme { get; set; }
    public decimal IngresosMensuales { get; set; }
    public decimal? MontoPensionHijos { get; set; }
    public decimal PorcentajePensionAlimentacion { get; set; }
    public int EsReestructura { get; set; }
    public string ContratosReestructura { get; set; } = string.Empty;
    public string FechaNacimiento { get; set; } = string.Empty;

    public bool SeguroGastosFunerarios { get; set; }
}
