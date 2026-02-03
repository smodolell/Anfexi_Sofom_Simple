using System.ComponentModel.DataAnnotations.Schema;
using System.Net.ServerSentEvents;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

public class ConfirmarCotizacionDto
{
    public List<SolicitudDto> lista { get; set; }

    public string folio { get; set; }
}

public class SolicitudDto
{
    public string Folio { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string CURP { get; set; }
    public string NSS { get; set; }
    public string NumeroEmpleado { get; set; }
    public string estadoSolicitud { get; set; }
    public string estadoFase { get; set; }
    public string folioSuapSipre { get; set; }
    public string esReestructura { get; set; }
    [NotMapped]
    public List<FaseItem> HistoriaFases { get; set; }
}

public class FaseItem
{
    public string Fase { get; set; }
    public string EstadoFase { get; set; }
    public string fechaOperacion { get; set; }
    public string comentario { get; set; }
}
