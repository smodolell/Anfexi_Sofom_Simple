namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class COT_Cotizador
{

    public int IdCotizador { get; set; }

    public int? IdAgencia { get; set; }

    public string? NombreCliente { get; set; }

    public string? RFC { get; set; }
    public string? CURP { get; set; }
    public string? NSS { get; set; }

    public int? IdPersonaJuridica { get; set; }

    public int IdPlan { get; set; }

    public DateTime? FechaIngresoEmpresa { get; set; }

    public DateTime FechaSimulacion { get; set; }

    public decimal SueldoNetoMensual { get; set; }

    public decimal? DescuentosFijosMensual { get; set; }

    public decimal? SueldoDisponible { get; set; }

    public decimal MontoSolicitar { get; set; }

    public int? FrecuenciaPago { get; set; }

    public int IdPlazo { get; set; }

    public decimal NumeroPagosFijos { get; set; }

    public decimal? PagoFijoTotalAproximado { get; set; }

    public decimal? PagoFijoTotalAproximadoMensual { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public int? IdConvenio { get; set; }

    public decimal? CAT { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? ClaveRastreoBancaria { get; set; }

    public bool? ValidaClaveRastreoBancaria { get; set; }

    public bool? Reestructurado { get; set; }

    public int? IdTipoCotizacion { get; set; }

    public int? IdTasa { get; set; }

    public decimal? Tasa { get; set; }

    public decimal? MontoPension { get; set; }

    public decimal? MontoPrestamo { get; set; }

    public string Folio { get; set; }

    public decimal? MontoQDeceaPagar { get; set; }

    public decimal? CalculoCAT { get; set; }

    public int? NumeroEmpleado { get; set; }

    public int? IdTipoPension { get; set; }

    public decimal? MontoPensionOrfandad { get; set; }

    public int? IdNomina { get; set; }

    public decimal? CapacidadPagoInforme { get; set; }

    public decimal? PorcentajeSeguro { get; set; }

    public bool? PrestamoVoz { get; set; }

    public decimal? Seguro { get; set; }

    public bool? SeguroGastosFunerarios { get; set; }

    public int? IdTipoSeguro { get; set; }

    public string? SuapSipre { get; set; }

    //public string Nss { get; set; }

    public string? GrupoPago { get; set; }

    //public string Curp { get; set; }

    public bool? EsOneClick { get; set; }

    public ICollection<COT_Cotizador_BeneficiosAdicionales> COT_Cotizador_BeneficiosAdicionales { get; set; } = new List<COT_Cotizador_BeneficiosAdicionales>();

    public ICollection<COT_SolicitudCotizador> COT_SolicitudCotizador { get; set; } = new List<COT_SolicitudCotizador>();

    public ICollection<COT_TablaAmortiza> COT_TablaAmortiza { get; set; } = new List<COT_TablaAmortiza>();

    public COT_Agencia IdAgenciaNavigation { get; set; }

    public COT_Plan IdPlanNavigation { get; set; }

    public ICollection<OT_Solicitud> OT_Solicitud { get; set; } = new List<OT_Solicitud>();
}