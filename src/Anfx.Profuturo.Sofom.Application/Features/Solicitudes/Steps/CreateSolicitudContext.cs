using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class CreateSolicitudContext
{
    public string Folio { get; set; } = "";

    public int IdSolicitud { get; set; }
    public int IdSolicitante { get; set; }
    public bool EsReestructuracion { get; set; }
    public required SolicitudModel Model { get; set; }

}
