namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class RC_Proceso
{

    public int IdProceso { get; set; }

    public string NombreProceso { get; set; }

    public string ClaveProceso { get; set; }

    public string Controlador { get; set; }

    public string Accion { get; set; }

    public string spAprobacion { get; set; }

    public string spCancelacion { get; set; }

    public int Orden { get; set; }

    public int IdEstatusSolicitud_Requiere { get; set; }
}