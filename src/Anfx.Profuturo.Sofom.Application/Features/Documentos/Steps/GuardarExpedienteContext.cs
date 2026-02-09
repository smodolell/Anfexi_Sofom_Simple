namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

public class GuardarExpedienteContext
{
    public string Folio { get; set; } = "";

    public int IdExpediente { get; set; }

    public int IdTipoDocumento { get; set; }
    public string Nombre { get; set; }
    public string MIME { get; set; }
    //public string Documento { get; set; }
    public string Documento { get; set; }




    public int IdExpedienteDocumento { get; set; }
}