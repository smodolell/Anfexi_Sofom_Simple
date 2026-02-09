using System.ComponentModel.DataAnnotations;

namespace Anfx.Profuturo.Sofom.Api.Requests.Documentos;

public class GuardarExpedienteRequest
{

    /// <summary>
    /// Folio de la Solicitud
    /// </summary>
    [Required]
    public string Folio { get; set; } = "";


    /// <summary>
    /// Tipo de Documento
    /// </summary>
    [Required]
    public int IdTipoDocumento { get; set; }
    /// <summary>
    /// </summary>
    [Required]
    public string Nombre { get; set; } = "";
    public string MIME { get; set; } = "";
    public string Documento { get; set; } = "";
}

