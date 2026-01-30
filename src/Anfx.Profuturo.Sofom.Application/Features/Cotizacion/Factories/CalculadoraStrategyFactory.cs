using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Factories;

public class CalculadoraStrategyFactory : ICalculadoraStrategyFactory
{
    private readonly IEnumerable<ICalculadoraStrategy> _calculadoras;

    public CalculadoraStrategyFactory(IEnumerable<ICalculadoraStrategy> calculadoras)
    {
        _calculadoras = calculadoras;
    }

    public ICalculadoraStrategy CrearCalculadora(CalculoContextDto context)
    {
        var strategy = _calculadoras.FirstOrDefault(s => s.AplicaPara(context));

        if (strategy == null)
        {
            throw new InvalidOperationException(
                $"No se encontró estrategia para TipoCotizacion: {context.TipoCotizacion}, " +
                $"Agencia: {context.IdAgencia}");
        }

        return strategy;
    }
}
