using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

public class ConfirmarCotizacionContext
{
    public int IdCotizador { get; set; }
    public int IdSolicitud { get; set; }

    public string FolioConfirmacion { get; set; } = "";

    public required CotizadorOpcionDto Opcion { get; set; }
}
