namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Interfaces;

public interface ISolicitudService
{
    Task<Result<int>> CreateSolicitudAsync(int idCotizador);


    Task<Result> GuardarFaseRees(int idsolicitud, string claveFase, bool completo);

}
