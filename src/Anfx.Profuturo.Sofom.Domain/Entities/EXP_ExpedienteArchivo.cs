

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class EXP_ExpedienteArchivo
{

    public int IdExpedienteArchivo { get; set; }

    public int IdExpedienteDocumento { get; set; }

    public string NombreUnico { get; set; }

    public string NombreReal { get; set; }

    public string Extension { get; set; }
    public string DatoExtra { get; set; }

    public string ContentType { get; set; }

    public DateTime FechaSubida { get; set; }

    public int ContentLength { get; set; }

    public EXP_ExpedienteDocumento IdExpedienteDocumentoNavigation { get; set; }
}