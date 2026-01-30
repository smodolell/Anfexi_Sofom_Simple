using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface ICalculadoraStrategyFactory
{
    ICalculadoraStrategy CrearCalculadora(CalculoContextDto context);
}
