using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Calculadora;

// 1. CapacidadDescuento
public class CalculadoraCapacidadDescuento : CalculadoraStrategyBase
{
    public CalculadoraCapacidadDescuento(IApplicationDbContext dbContext, ICalculoHelper calculoHelper) : base(dbContext, calculoHelper)
    {
    }

    public override bool AplicaPara(CalculoContextDto context)
    {
        return (CotizacionConstants.TipoCotizacion.EsCapacidadDescuento(context.TipoCotizacion) && !context.EsIMSS);
    }

    public override async Task<CotizadorOpcionDto> GetOpcion(CalculoContextDto context)
    {
        await LoadData(context);

        if (context.Plan == null || context.Plazo == null || context.Periodicidad == null)
        {
            throw new Exception("Datos de plan, plazo o periodicidad no encontrados.");
        }

        context.IngresosMensuales = (context.CapacidadPagoInforme * 100) / 30;
        context.MontoPrestamo = context.CapacidadPagoInforme;


        decimal montoBase = context.MontoPensionHijos > 0
            ? context.MontoPrestamo - (context.MontoPensionHijos * (context.PorcentajePencion / 100))
            : context.MontoPrestamo;

        // Calcular suma de flujos descontados
        decimal tasaDescuento = (decimal)_calculoHelper.CalcularTasaPeriodica(
            context.InteresAnualIVA,
            context.Periodicidad.NroPagosAnio);

        int numeroPagos = _calculoHelper.CalcularNumeroPagos(
            context.Plazo.Plazo,
            context.Periodicidad.NroPagosAnio);

        decimal montoPrestamo = _calculoHelper.CalcularSumaFlujosDescontados(
            montoBase,
            tasaDescuento,
            numeroPagos);

        // Ajustar por porcentaje de pensión
        if (context.MontoPensionHijos < 1)
        {
            montoPrestamo = montoPrestamo * (context.PorcentajePencion / 100);
        }

        // Calcular descuento mensual para otras agencias
        decimal descuentoMensual = _calculoHelper.CalcularPagoPeriodico(
            montoPrestamo,
            tasaDescuento,
            context.Plazo.Plazo);

        return ConstruirOpcionBase(context, montoPrestamo, descuentoMensual);
    }
}
