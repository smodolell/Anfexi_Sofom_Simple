namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class EXP_ExpedienteDocumento
{

    public int IdExpedienteDocumento { get; set; }

    public int IdExpediente { get; set; }

    public int IdDocumentoConfig { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstadoDocumento { get; set; }

    public string Comentario { get; set; }

    public DateTime? FechaUltimaModificacion { get; set; }

    public DateTime? FechaVigencia { get; set; }

    public ICollection<EXP_ExpedienteArchivo> EXP_ExpedienteArchivo { get; set; } = new List<EXP_ExpedienteArchivo>();

    public EXP_DocumentoConfig IdDocumentoConfigNavigation { get; set; }

    public EXP_Expediente IdExpedienteNavigation { get; set; }
}