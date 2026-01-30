using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
public interface ITablaAmortizacionService
{
    Task<List<TablaAmortizaItemDto>> GenerarTablaAmortizacionAsync(
        int idPlan,
        int idPlazo,
        int idAgencia,
        int idPeriodicidad,
        decimal montoSolicitar);

    Task<int> GuardarTablaAmortizacionAsync(
        int idCotizador,
        List<TablaAmortizaItemDto> tablaAmortizacionDto
    );

    Task<FrecuenciaPagoDto> ObtenerFrecuenciaPagoAsync(int idAgencia, int idPeriodicidad);
}

