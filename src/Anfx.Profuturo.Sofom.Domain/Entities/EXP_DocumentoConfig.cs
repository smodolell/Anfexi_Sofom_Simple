namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class EXP_DocumentoConfig
{

    public int IdDocumentoConfig { get; set; }

    public int IdDocumento { get; set; }

    public int IdTipoPersonaAplica { get; set; }

    public string? Referencia { get; set; }

    public bool Activo { get; set; }

    public ICollection<EXP_ExpedienteDocumento> EXP_ExpedienteDocumento { get; set; } = new List<EXP_ExpedienteDocumento>();
}