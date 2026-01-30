using Anfx.Profuturo.Sofom.Domain.Entities;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

public class CalculoContextDto
{
    public int IdPlan { get; set; }
    public int IdPlazo { get; set; }
    public int IdPeriodicidad { get;  } = CotizacionConstants.ID_PERIODICIDAD_MENSUAL;
    public int IdPension { get; set; }
    public int IdAgencia { get; set; }

    public int TipoCotizacion { get; set; }
    public decimal MontoPensionHijos { get; set; }
    public decimal? CapacidadPago { get; set; }
    public decimal PorcentajeSeguro { get; set; }
    public decimal PorcentajePencion { get; set; }
    public bool SeguroGastosFunerarios { get; set; }

    public int IdTipoSeguro { get; set; }
    public decimal ValorTasa { get; set; }

    public string FechaNacimiento { get; set; }




    public bool EsReestructura { get; set; }
    public string ContratosReestructura { get; set; } = "";
    public string FolioConfirmacion { get; set; } = "";


    public COT_Plan? Plan { get; set; }
    public COT_Plazo? Plazo { get; set; }
    public COT_Tasa? Tasa { get; set; }
    public COT_Comision? Comision { get; set; }
    public SB_Periodicidad? Periodicidad { get; set; }


    public bool TienePensionHijos => MontoPensionHijos >= 1;


    public bool EsIMSS => IdAgencia == CotizacionConstants.ID_AGENCIA_IMSS;

    public decimal InteresAnualIVA { get; set; }


    public int ValorPlazo => _valorPlazo();

    private int _valorPlazo()
    {
        if(Plazo == null)
            return 0;
        else
            return Plazo.Plazo;
    }

    public int EdadSolicitante => _calcularEdad();

    public int _calcularEdad()
    {
        if (!string.IsNullOrEmpty(FechaNacimiento))
        {
            var fechaNac = DateTime.Parse(FechaNacimiento, new CultureInfo("es-MX"));
            return new DateTime((DateTime.Now - fechaNac).Ticks).Year - 1;
        }
        return 0;
    }


    public decimal IngresosMensuales { get; set; }
    public decimal CapacidadPagoInforme { get; set; }
    public decimal MontoPrestamo { get; set; }

}
