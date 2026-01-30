namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class BitacoraCodigos
{
    public int IdBitacora { get; set; }
    public int? IdSolicitud { get; set; }
    public string Folio { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoPromotor { get; set; } = string.Empty;
    public string XMLOriginial { get; set; } = string.Empty;
    public string TelefonoCliente { get; set; } = string.Empty;
    public string TelefonoAsesor { get; set; } = string.Empty;
}