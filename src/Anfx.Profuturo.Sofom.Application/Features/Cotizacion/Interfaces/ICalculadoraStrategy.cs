using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface ICalculadoraStrategy
{
    Task<CotizadorOpcionDto> GetOpcion(CalculoContextDto context);

    bool AplicaPara(CalculoContextDto context);
}
