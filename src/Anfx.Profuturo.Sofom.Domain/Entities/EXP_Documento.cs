

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class EXP_Documento
{

    public int IdDocumento { get; set; }

    public int IdTipoDocumentacion { get; set; }

    public string NombreDocumento { get; set; }

    public string Descripcion { get; set; }

    public decimal? Orden { get; set; }

    public bool Activo { get; set; }

    public int? IdAgencia { get; set; }

    public string NombreArchivo { get; set; }

    public EXP_TipoDocumentacion IdTipoDocumentacionNavigation { get; set; }
}