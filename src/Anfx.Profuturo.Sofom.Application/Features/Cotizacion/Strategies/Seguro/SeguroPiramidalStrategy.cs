using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Seguro;

public class SeguroPiramidalStrategy : ISeguroStrategy
{
    public bool AplicaPara(int idTipoSeguro) => idTipoSeguro == 1;

    public decimal CalcularSeguro(decimal montoPrestamo, decimal porcentajeSeguro, bool seguroGF)
    {
        return montoPrestamo * (porcentajeSeguro / 100);
    }
}
