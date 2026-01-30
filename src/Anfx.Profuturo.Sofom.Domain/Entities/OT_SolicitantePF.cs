

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_SolicitantePF
{

    public int IdSolicitante { get; set; }

    public string ApellidoPaterno { get; set; }

    public string ApellidoMaterno { get; set; }

    public int IdGenero { get; set; }

    public string Nacionalidad { get; set; }

    public int IdEstadoCivil { get; set; }

    public int? NoDependientes { get; set; }

    public bool TieneAutoPropio { get; set; }

    public int? CuantosAutos { get; set; }

    public bool PagaInstFinanciera { get; set; }

    public string NombreInstFinanciera { get; set; }

    public bool EsTitularTarjCr { get; set; }

    public string Institucion { get; set; }

    public string CURP { get; set; }

    public string PaisNacimiento { get; set; }

    public string CiudadNacimiento { get; set; }

    public string Fiel { get; set; }

    public string GradoEstudios { get; set; }

    public string TipoIdentificacion { get; set; }

    public string Folio { get; set; }

    public string FormaMigratoria { get; set; }

    public int AniosEnElPais { get; set; }

    public string RegimenMatrimonial { get; set; }

    public bool ConyugePercibeIngresos { get; set; }

    public string TelefonoCasa { get; set; }

    public string Nextel { get; set; }

    public string TelefonoCelular { get; set; }

    public int? GrupoPago { get; set; }

    public string ClaveDelegacionPago { get; set; }

    public string IdEntidadFederativa { get; set; }

    public long? NSS { get; set; }

    public int? IdDependenciaIMSS { get; set; }

    public string Pais { get; set; }

    public string OcupacionOGiro { get; set; }

    public string NumeroSerieElectronica { get; set; }

    public string CargoFederal { get; set; }

    public string CargoFederalPariente { get; set; }

    public int? AutorizoDatosPersonales { get; set; }

    public string PreguntaEstadoCuenta { get; set; }

    public int? IdCiaTelefonica { get; set; }

    public bool? PrivacidadNomina { get; set; }

    public DateTime? FechaPrivacidadNomina { get; set; }

    public CompaniaTelefonica IdCiaTelefonicaNavigation { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }
}