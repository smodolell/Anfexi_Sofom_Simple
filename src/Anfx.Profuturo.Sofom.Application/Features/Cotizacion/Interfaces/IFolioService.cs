namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface IFolioService
{
    Task<string> GenerarFolioAsync(int idCotizador, int idAgencia);
    
}


