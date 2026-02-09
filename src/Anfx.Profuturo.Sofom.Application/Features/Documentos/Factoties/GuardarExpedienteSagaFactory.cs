using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;
using Microsoft.Extensions.DependencyInjection;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Factoties;

public class GuardarExpedienteSagaFactory : IGuardarExpedienteSagaFactory
{
    private readonly IServiceProvider _serviceProvider;

    public GuardarExpedienteSagaFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public ISagaOrchestrator<GuardarExpedienteContext> GuardarExpedienteSaga()
    {
        var saga  = new SagaOrchestrator<GuardarExpedienteContext>();


        saga.AddStep(CreateStep<StepCrearExpediente>());
        saga.AddStep(CreateStep<StepGuardarDocumento>());
        saga.AddStep(CreateStep<StepActualizarEstatusExpediente>());
        saga.AddStep(CreateStep<StepActualizarFaseExpediente>());


        return saga;
    }
    private ISagaStep<GuardarExpedienteContext> CreateStep<TStep>()
       where TStep : ISagaStep<GuardarExpedienteContext>
    {
        return _serviceProvider.GetRequiredService<TStep>();
    }
}
