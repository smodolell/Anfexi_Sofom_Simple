

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class URL_DatosSolicitud
{

    public int Id { get; set; }

    public string Folio { get; set; }

    public bool? AvisoPrivacidad { get; set; }

    public string Ubicacion { get; set; }

    public bool? PermisoCamara { get; set; }

    public bool? PermisoMicrofono { get; set; }

    public string FechaInforme { get; set; }

    public string ConceptoInforme { get; set; }

    public string FolioConfirmacion { get; set; }

    public string DummyField1 { get; set; }

    public string DummyField2 { get; set; }

    public string DummyField3 { get; set; }

    public string DummyField4 { get; set; }

    public string DummyField5 { get; set; }
}