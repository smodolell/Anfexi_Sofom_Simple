using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface IOpcionValidator
{
    Task<ValidacionOpcionResult> ValidarOpcion(
        CotizadorOpcionDto opcion,
        CalculoContextDto context
    );
}

