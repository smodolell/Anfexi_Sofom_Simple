using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Seguro;

public class SeguroGastosFunerariosStrategy : ISeguroStrategy
{
    public bool AplicaPara(int idTipoSeguro) => idTipoSeguro == 2;

    public decimal CalcularSeguro(decimal montoPrestamo, decimal porcentajeSeguro, bool seguroGF)
    {
        return seguroGF ? montoPrestamo * (porcentajeSeguro / 100) : 0;
    }
}
