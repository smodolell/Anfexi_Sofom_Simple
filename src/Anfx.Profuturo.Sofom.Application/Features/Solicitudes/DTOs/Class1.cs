namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.DTOs;

public class SolicitudModel
{
    public SolicitudModel()
    {
        Persona = new PersonaModel();
        Domicilio = new DomicilioModel();
    }

    public string NumeroEmpleadoVendedor { get; set; }
    public PersonaModel Persona { get; set; }
    public DomicilioModel Domicilio { get; set; }
    public bool AvisoPrivacidad { get; set; }
    public string Ubicacion { get; set; }
    public bool PermisoUsoCamara { get; set; }
    public bool PermisoUsoMicrofono { get; set; }
    public string FechaInforme { get; set; }
    public string ConceptosInforme { get; set; }
    public string FolioConfirmacion { get; set; }
    public string CodigoError { get; set; }
    public string Folio { get; set; }

}

public class PersonaModel
{
    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string PrimerNombre { get; set; }
    public string SegundoNombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Genero { get; set; }
    public string GrupodePago { get; set; }
    public string ClaveDelegacionPago { get; set; }
    public string EntidadNacimiento { get; set; }
    public string PaisNacimiento { get; set; }
    public string Nacionalidad { get; set; }
    public string FechaNacimiento { get; set; }
    public string CURP { get; set; }
    public string RFC { get; set; }
    public string NSS { get; set; }
    public string DependenciaIMSS { get; set; }
    public string Ocupacion { get; set; }
    public string Pregunta1PLD { get; set; }
    public string Cargo2PLD { get; set; }
    public string Autorizadatos { get; set; }
    public string TipoEdoCta { get; set; }
    public string Banco { get; set; }
    public string CuentaClabe { get; set; }
    public decimal IngresoMensual { get; set; }
    public int PensionHijos { get; set; }
    public decimal CapacidadPago { get; set; }
    public string FIEL { get; set; }
    public string NumeroEmpleado { get; set; }
    public decimal MontoPension { get; set; }
    public string FechaPagoPension { get; set; }
    public string NumeroOferta { get; set; }
    public string FolioTramite { get; set; }
    public string Cuenta { get; set; }
    public string NoSucursalBancaria { get; set; }
    public int? AñosAntiguedad { get; set; }

}
public class DomicilioModel
{
    public string Calle { get; set; }
    public string NumExt { get; set; }
    public string NumInt { get; set; }
    public string CP { get; set; }
    public string Estado { get; set; }
    public string Delegacion { get; set; }
    public int IdColonia { get; set; }
    public string Colonia { get; set; }
    public string Ciudad { get; set; }
    public string Pais { get; set; }
    public string Telefono { get; set; }
    public string Celular { get; set; }
    public string CiaTelefonica { get; set; }
    public string Correo { get; set; }
}