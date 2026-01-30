

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_Solicitante_InfoPension
{

    public int IdInfoPension { get; set; }

    public int IdSolicitante { get; set; }

    public string NumeroEmpleado { get; set; }

    public decimal? MontoPension { get; set; }

    public DateTime? FechaPagoPension { get; set; }

    public int? AñosAntiguedad { get; set; }

    public string NumeroOferta { get; set; }

    public string FolioIdentificador { get; set; }

    public DateTime? FechaJubilacionPension { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }
}