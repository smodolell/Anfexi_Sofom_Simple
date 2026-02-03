using Anfx.Profuturo.Sofom.Application.Features.Contratos.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Contratos.Services;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Factories;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Calculadora;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Coordinators;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Seguro;
using FluentValidation;
using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Anfx.Profuturo.Sofom.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddValidatorsFromAssemblyContaining<Dummy>();

        services.AddLiteBus(configuration =>
        {
            var assembly = typeof(DependencyInjection).Assembly;

            configuration.AddCommandModule(m => m.RegisterFromAssembly(assembly));
            configuration.AddQueryModule(m => m.RegisterFromAssembly(assembly));

        });




        //// Register MediatR for CQRS pattern    
        //services.AddMediatR(configuration=> {
        //    configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        //});
        //// Register AutoMapper for object mapping     
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());


        //// Calculadora strategies 
        services.AddScoped<ICalculoHelper, CalculoHelper>();
        services.AddScoped<IOpcionValidator, OpcionValidator>();
        services.AddScoped<ICalculadoraStrategy, CalculadoraCapacidadDescuento>();
        services.AddScoped<ICalculadoraStrategy, CalculadoraMontoPrestamo>();

        // Estrategias de seguro 
        services.AddScoped<ISeguroStrategy, SeguroPiramidalStrategy>();
        services.AddScoped<ISeguroStrategy, SeguroGastosFunerariosStrategy>();
        services.AddScoped<ISeguroStrategy, SinSeguroStrategy>();

        //services.AddScoped<ICotizadorService, CotizadorService>();
        //services.AddScoped<ISolicitudService, SolicitudService>();
        services.AddScoped<IFolioService, FolioService>();
        services.AddScoped<IReestructuracionService, ReestructuracionService>();

        // Registrar factory y coordinador
        services.AddScoped<ICalculadoraStrategyFactory, CalculadoraStrategyFactory>();


        services.AddScoped<IConfirmarCotizacionSagaFactory,ConfirmarCotizacionSagaFactory>();
        services.AddScoped<StepReestructura>();
        services.AddScoped<StepReestructuraUpdateSolicitd>();
        services.AddScoped<StepReestructuraUpdateFase>();
        services.AddScoped<StepReestructuraObtenerFolio>();
        services.AddScoped<StepCrearCotizador>();
        services.AddScoped<StepCrearCotizador>();
        services.AddScoped<StepGenerarTablaAmortizacion>();

        services.AddScoped<CotizacionCoordinator>();

        //Services 
     
        services.AddScoped<ITablaAmortizacionService, TablaAmortizacionService>();

        //services.AddTransient(typeof(ISagaOrchestrator<>), typeof(SagaOrchestrator<>));

        ////Solicitud
        //services.AddScoped<ISolicitudSagaFactory, SolicitudSagaFactory>();
        //services.AddScoped<ActualizarFaseReestructuraStep>();
        //services.AddScoped<ActualizarFaseStep>();
        //services.AddScoped<ActualizarSolicitanteStep>();
        //services.AddScoped<ActualizarSolicitudReestructuraStep>();
        //services.AddScoped<BuscarColoniaStep>();
        //services.AddScoped<CrearExpedienteStep>();
        //services.AddScoped<CrearSolicitanteStep>();
        //services.AddScoped<CreateSolicitudContext>();
        //services.AddScoped<GeneraUsuarioOneClicStep>();
        //services.AddScoped<GuardarDatosAdicionalesStep>();

        return services;
    }
}
class Dummy { }