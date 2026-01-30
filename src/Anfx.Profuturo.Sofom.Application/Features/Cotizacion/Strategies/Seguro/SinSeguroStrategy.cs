using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Seguro;

public class SinSeguroStrategy : ISeguroStrategy
{
    public bool AplicaPara(int idTipoSeguro) => idTipoSeguro == 3;

    public decimal CalcularSeguro(decimal montoPrestamo, decimal porcentajeSeguro, bool seguroGF)
    {
        return 0;
    }
}
