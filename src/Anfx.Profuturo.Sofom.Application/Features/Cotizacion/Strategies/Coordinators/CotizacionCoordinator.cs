using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;
using Ardalis.Result;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Coordinators;

public class CotizacionCoordinator
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICalculadoraStrategyFactory _calculadoraFactory;
    private readonly IEnumerable<ISeguroStrategy> _seguroStrategies;
    private readonly ICalculoHelper _calculoHelper;
    private readonly ITablaAmortizacionService _tablaAmortizacionService;
    private readonly IOpcionValidator _opcionValidator;

    public CotizacionCoordinator(
        IApplicationDbContext dbContext,
        ICalculadoraStrategyFactory calculadoraFactory,
        IEnumerable<ISeguroStrategy> seguroStrategies,
        ICalculoHelper calculoHelper,
        ITablaAmortizacionService tablaAmortizacionService,
        IOpcionValidator opcionValidator)
    {
        _dbContext = dbContext;
        _calculadoraFactory = calculadoraFactory;
        _seguroStrategies = seguroStrategies;
        _calculoHelper = calculoHelper;
        _tablaAmortizacionService = tablaAmortizacionService;
        _opcionValidator = opcionValidator;
    }

    public async Task<Result<CotizadorOpcionDto>> CalcularOpcion(CalculoContextDto context)
    {
        // 1. Obtener calculadora principal
        var calculadora = _calculadoraFactory.CrearCalculadora(context);

        // 2. Calcular opción base
        var opcion = await calculadora.GetOpcion(context);

        // 3. Ajustar seguro según estrategia
        var seguroStrategy = _seguroStrategies.FirstOrDefault(s => s.AplicaPara(context.IdTipoSeguro));
        if (seguroStrategy != null)
        {
            opcion.Seguro = seguroStrategy.CalcularSeguro(
                opcion.MontoPrestamo,
                context.PorcentajeSeguro,
                context.SeguroGastosFunerarios);

        }
        opcion.idPlan = context.IdPlan;
        opcion.PrestamoMenosSeguro = opcion.MontoPrestamo - opcion.Seguro;
        opcion.MontoAproximadoDeposito = opcion.MontoPrestamo - opcion.Seguro;

        opcion.TablaAmortiza = await _tablaAmortizacionService.GenerarTablaAmortizacionAsync(
         context.IdPlan,
         context.IdPlazo,
         context.IdAgencia,
         context.IdPeriodicidad,
         opcion.MontoPrestamo);


        var saldos = await _calculoHelper.CalcularSaldos(context.EsReestructura, context.ContratosReestructura);
        opcion.MontoAproximadoDeposito = opcion.MontoAproximadoDeposito - saldos;

        var validacion = await _opcionValidator.ValidarOpcion(opcion,context);

        opcion.FolioConfirmacion = opcion.FolioConfirmacion ?? "0";
        opcion.EsReestructura = context.EsReestructura ? 1 : 0;
        opcion.ContratosReestructura = context.ContratosReestructura ?? "";

        if (opcion.TablaAmortiza?.Count > 0)
        {
            opcion.DescuentoMensual = Math.Round(opcion.TablaAmortiza[0].Total, 2);
        }

        // 9. Retornar resultado con validación
        if (!validacion.EsValida)
        {
            return Result.Invalid(
                validacion.Errores
                    .Select(s=> new ValidationError(s.Codigo.ToString(),s.Mensaje))
            );
        }
        return opcion;
    }


}
