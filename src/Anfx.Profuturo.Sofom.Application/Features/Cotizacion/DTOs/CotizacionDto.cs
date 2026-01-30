using Anfx.Profuturo.Sofom.Application.Common.Dtos;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

public class CotizacionDto
{
    public int IdCotizacion { get; set; }
    public int TipoCotizacion { get; set; }
    public string FechaNacimiento { get; set; } = string.Empty;
    public decimal IngresosMensuales { get; set; }
    //public int PensionHijos { get; set; }
    public decimal MontoPensionHijos { get; set; }
    public int TipoPension { get; set; }
    public decimal CapacidadPagoInforme { get; set; }
    public decimal MontoPrestamo { get; set; }
    public string Validaciones { get; set; } = string.Empty;

    public bool SeguroGastosFunerarios { get; set; }
    public int? EsReestructura { get; set; }
    public string ContratosReestructura { get; set; } = string.Empty;
    public int IdPlan { get; set; }
    public decimal PorcentajePensionAlimentacion { get; set; }
    public decimal MontoMinimo { get; set; }
    public decimal MontoMaximo { get; set; }
    public List<CotizadorOpcionDto> Opciones { get; set; } = new List<CotizadorOpcionDto>();
    public List<ErrorDto> Errores { get; set; } = new List<ErrorDto>();
}
