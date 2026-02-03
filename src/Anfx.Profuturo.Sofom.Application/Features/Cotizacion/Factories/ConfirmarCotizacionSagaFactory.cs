using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;
using Microsoft.Extensions.DependencyInjection;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Factories;

public class ConfirmarCotizacionSagaFactory : IConfirmarCotizacionSagaFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ConfirmarCotizacionSagaFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISagaOrchestrator<ConfirmarCotizacionContext> ConfirmarCotizacionSaga(bool esReestructuracion)
    {
        var saga = new SagaOrchestrator<ConfirmarCotizacionContext>();

        if (esReestructuracion)
        {
            saga.AddStep(CreateStep<StepReestructura>());
            saga.AddStep(CreateStep<StepReestructuraUpdateSolicitd>());
            saga.AddStep(CreateStep<StepGenerarTablaAmortizacion>());
            saga.AddStep(CreateStep<StepReestructuraUpdateFase>());
            saga.AddStep(CreateStep<StepReestructuraObtenerFolio>());
        }
        else
        {
            saga.AddStep(CreateStep<StepCrearCotizador>());
            saga.AddStep(CreateStep<StepCrearSolicitud>());
            saga.AddStep(CreateStep<StepGenerarTablaAmortizacion>());
        }
        saga.AddStep(CreateStep<StepGenerarCAT>());
        return saga;
    }

    private ISagaStep<ConfirmarCotizacionContext> CreateStep<TStep>()
        where TStep : ISagaStep<ConfirmarCotizacionContext>
    {
        return _serviceProvider.GetRequiredService<TStep>();
    }
}
