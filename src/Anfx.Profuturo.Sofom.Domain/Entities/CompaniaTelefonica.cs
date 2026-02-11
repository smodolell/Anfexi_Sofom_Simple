namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class CompaniaTelefonica
{
    public int IdCiaTelefonica { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public ICollection<OT_SolicitantePF> OT_SolicitantePF { get; set; } = new List<OT_SolicitantePF>();
}