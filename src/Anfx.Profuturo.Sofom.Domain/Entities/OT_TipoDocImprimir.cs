

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_TipoDocImprimir
{

    public int IdTipoDocImprimir { get; set; }

    public string Titulo { get; set; }

    public string NombreArchivo { get; set; }

    public decimal Orden { get; set; }

    public bool EsSoloLectura { get; set; }

    public int? IdAgencia { get; set; }

    public string NombreIdentificacionArchivo { get; set; }
}