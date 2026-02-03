using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface IConfirmarCotizacionSagaFactory
{
    ISagaOrchestrator<ConfirmarCotizacionContext> ConfirmarCotizacionSaga(bool esReestructuracion);
}
