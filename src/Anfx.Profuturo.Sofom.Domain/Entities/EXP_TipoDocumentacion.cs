

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class EXP_TipoDocumentacion
{

    public int IdTipoDocumentacion { get; set; }

    public string Titulo { get; set; }

    public decimal? Orden { get; set; }

    public ICollection<EXP_Documento> EXP_Documento { get; set; } = new List<EXP_Documento>();
}