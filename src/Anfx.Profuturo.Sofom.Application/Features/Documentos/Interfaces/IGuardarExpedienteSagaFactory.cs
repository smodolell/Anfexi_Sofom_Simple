using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface IGuardarExpedienteSagaFactory
{
    ISagaOrchestrator<GuardarExpedienteContext> GuardarExpedienteSaga();
}
