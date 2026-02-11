namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

public class GuardarExpedienteContext
{
    public string Folio { get; set; } = "";

    public int IdExpediente { get; set; }

    public int IdTipoDocumento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string MIME { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;




    public int IdExpedienteDocumento { get; set; }
}