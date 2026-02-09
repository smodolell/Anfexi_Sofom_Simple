namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class CatTiposMimeDocumento
{
    public int Id { get; set; }

    public string Extencion { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public string TipoMIME { get; set; } =string.Empty;
} 