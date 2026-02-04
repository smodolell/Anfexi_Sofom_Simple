namespace Anfx.Profuturo.Sofom.Application.Common.Dtos;


public class DocumentoConfigItem
{
    public int IdExpedienteDocumento { get; set; }
    public int IdExpediente { get; set; }
    public int IdDocumentoConfig { get; set; }
    public int IdUsuario { get; set; }
    public int IdEstadoDocumento { get; set; }
    public string NombreDocumento { get; set; } = "";
    public string EstadoDocumento { get; set; } = "";
    public string Icono { get; set; } = "";
    public string Grupo { get; set; } = "";
    public DateTime? FechaUltimaModificacion { get; set; }
    public DateTime? FechaVigencia { get; set; }
    public string Comentario { get; set; } = "";
}
