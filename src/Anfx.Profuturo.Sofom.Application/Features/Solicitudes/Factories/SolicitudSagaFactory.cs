using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;
using Microsoft.Extensions.DependencyInjection;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Factories;

public class SolicitudSagaFactory : ISolicitudSagaFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SolicitudSagaFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISagaOrchestrator<CreateSolicitudContext> CreateSolicitudSaga(bool esReestructuracion)
    {
        var saga = new SagaOrchestrator<CreateSolicitudContext>();

        saga.AddStep(CreateStep<GeneraUsuarioOneClicStep>());
        saga.AddStep(CreateStep<BuscarColoniaStep>());

        
        if (esReestructuracion)
        {
            saga.AddStep(CreateStep<ActualizarSolicitanteStep>());
            saga.AddStep(CreateStep<ActualizarSolicitudReestructuraStep>());
            saga.AddStep(CreateStep<ActualizarFaseReestructuraStep>());
        }
        else
        {
            saga.AddStep(CreateStep<CrearSolicitanteStep>());
            saga.AddStep(CreateStep<ActualizarFaseStep>());
            saga.AddStep(CreateStep<CrearExpedienteStep>());
        }

        
        saga.AddStep(CreateStep<GuardarDatosAdicionalesStep>());
        

        return saga;
    }

    private ISagaStep<CreateSolicitudContext> CreateStep<TStep>()
        where TStep : ISagaStep<CreateSolicitudContext>
    {
        return _serviceProvider.GetRequiredService<TStep>();
    }
}
