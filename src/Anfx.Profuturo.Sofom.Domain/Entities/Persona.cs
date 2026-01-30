

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Persona
{

    public int IdPersona { get; set; }

    public string NombrePersona { get; set; }

    public string ApellidoPaterno { get; set; }

    public string ApellidoMaterno { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdPersonaJuridica { get; set; }

    public string RFC { get; set; }

    public string Identificador { get; set; }

    public DateTime? FechaAlta { get; set; }

    public string Email { get; set; }

    public int IdUnidadNegocio { get; set; }

    public int IdGenero { get; set; }

    public string IdEntidadFederativa { get; set; }

    public string PaisNacimiento { get; set; }

    public string Nacionalidad { get; set; }

    public long? NSS { get; set; }

    public string CURP { get; set; }

    public int? GrupoPago { get; set; }

    public int? IdDependenciaIMSS { get; set; }

    public string OcupacionOGiro { get; set; }

    public int? AutorizoDatosPersonales { get; set; }

    public string PreguntaEstadoCuenta { get; set; }

    public string NumeroSerieElectronica { get; set; }

    public int? IdBanco { get; set; }

    public string CuentaClabe { get; set; }

    public string EmpresaCliente { get; set; }

    public string NoEmpleadoCliente { get; set; }

    public string TipoPension { get; set; }

    public decimal? MontoPensionOrfandad { get; set; }

    public string Cuenta { get; set; }

    public string TipoNomina { get; set; }

    public ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();

    public ICollection<PersonaDireccion> PersonaDireccion { get; set; } = new List<PersonaDireccion>();

    public PersonaFisica PersonaFisica { get; set; }
}