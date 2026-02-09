

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_Fase
{

    public int IdFase { get; set; }

    public string ClaveUnica { get; set; } = string.Empty;  

    public string? NombreFase { get; set; }

    public int IdEstatusSolicitud_Requiere { get; set; }

    public string? Controlador { get; set; }

    public string? Accion { get; set; }

    public string? Icono { get; set; }

    public decimal Orden { get; set; }

    public string? ProcedimientoValidacion { get; set; }

    public string? MailAdicional { get; set; }
}