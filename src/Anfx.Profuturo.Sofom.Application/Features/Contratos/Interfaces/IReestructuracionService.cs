using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Contratos.Interfaces;

public interface IReestructuracionService
{
    Task<Result<ReestructuracionResult>> IniciarReestructuracion(string contratos, int idAsesor);
    Task<Result> ReestructurarUpdateSolicitd(int solicitudId, int idCotizador, CotizadorOpcionDto opcion);

}
public record ReestructuracionResult(int IdSolicitud, int IdCotizador, string Folio);
