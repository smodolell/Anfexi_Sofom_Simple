namespace Anfx.Profuturo.Sofom.Domain.Entities;
public partial class COT_SolicitudCotizador
{

    public int IdSolicitud { get; set; }

    public int IdCotizador { get; set; }

    public int? IdContrato { get; set; }

    public Contrato IdContratoNavigation { get; set; }

    public COT_Cotizador IdCotizadorNavigation { get; set; }

    public OT_Solicitud IdSolicitudNavigation { get; set; }
}