using System.ComponentModel;

namespace Anfx.Profuturo.Sofom.Api.Requests.Cotizacion;

public class SimularCotizacionRequest
{

    /// <summary>
    /// Tipo de Cotizacion
    /// </summary>    
    [DefaultValue(1)]
    public int TipoCotizacion { get; set; }

    /// <summary>
    /// Fecha de Nacimiento
    /// </summary>
    /// <example>01/01/2000</example>
    [DefaultValue("01/01/2000")]
    public string FechaNacimiento { get; set; } = string.Empty;

    /// <summary>
    /// Ingresos Mensuales
    /// </summary>
    public decimal IngresosMensuales { get; set; }


    /// <summary>
    /// Monto de la Pension por Hijos
    /// </summary>    
    public decimal MontoPensionHijos { get; set; }

    public decimal CapacidadPagoInforme { get; set; }

    [DefaultValue(1)]
    public int TipoPension { get; set; }

    /// <summary>
    /// Identificador del Plan
    /// </summary>
    public int? IdPlan { get; set; }




    /// <summary>
    /// Identificador del Plan
    /// </summary>
    public decimal MontoPrestamo { get; set; }

    public decimal? PorcentajePensionAlimentacion { get; set; }


    public int? EsReestructura { get; set; } = 0;
    public string? ContratosReestructura { get; set; } = string.Empty;

    public bool SeguroGastosFunerarios { get; set; }
}
