namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface ISeguroStrategy
{
    decimal CalcularSeguro(decimal montoPrestamo, decimal porcentajeSeguro, bool seguroGF);
    bool AplicaPara(int idTipoSeguro);
}
