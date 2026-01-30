namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class EXP_Expediente
{

    public int IdExpediente { get; set; }

    public int IdDuenioExpediente { get; set; }

    public int IdQuePersona { get; set; }

    public ICollection<EXP_ExpedienteDocumento> EXP_ExpedienteDocumento { get; set; } = new List<EXP_ExpedienteDocumento>();
}