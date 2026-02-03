using System.ComponentModel;

namespace Anfx.Profuturo.Sofom.Api.Requests.Cotizacion;

public class ConfirmarCotizacionRequest
{
    /// <summary>
    /// Identificador del Plan
    /// </summary>
    public int IdPlan { get; set; }

    /// <summary>
    /// Numero de Pagos
    /// </summary>
    public int Plazo { get; set; }


    public decimal MontoPrestamo { get; set; } 
    public decimal PorcentajeSeguro { get; set; } 
    public decimal DescuentoMensual { get; set; } 

    [DefaultValue(false)]
    public bool EsReestructura { get; set; }



    /// <summary>
    /// Si es es restructura tiene que en enviar los contratos que se van 
    /// a restructurar
    /// </summary>
    [DefaultValue("")]
    public string ContratosReestructura { get; set; } = "";
}
