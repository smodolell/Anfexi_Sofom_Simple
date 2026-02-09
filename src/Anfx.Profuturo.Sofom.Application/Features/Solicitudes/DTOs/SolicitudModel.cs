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
