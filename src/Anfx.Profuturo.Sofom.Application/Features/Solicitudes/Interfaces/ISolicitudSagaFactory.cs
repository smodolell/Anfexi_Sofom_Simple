using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Interfaces;

public interface ISolicitudSagaFactory
{
    ISagaOrchestrator<CreateSolicitudContext> CreateSolicitudSaga(bool esReestructuracion);
}
