namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class CompaniaTelefonica
{
    public int IdCiaTelefonica { get; set; }

    public string Descripcion { get; set; }

    public ICollection<OT_SolicitantePF> OT_SolicitantePF { get; set; } = new List<OT_SolicitantePF>();
}