using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Calculadora;

// 3. MontoPrestamo
public class CalculadoraMontoPrestamo : CalculadoraStrategyBase
{
    public CalculadoraMontoPrestamo(IApplicationDbContext dbContext, ICalculoHelper calculoHelper) : base(dbContext, calculoHelper)
    {
    }

    public override bool AplicaPara(CalculoContextDto context)
    {
        return CotizacionConstants.TipoCotizacion.EsMontoPrestamo(context.TipoCotizacion) && !context.EsIMSS;
    }

    public override async Task<CotizadorOpcionDto> GetOpcion(CalculoContextDto context)
    {
        await LoadData(context);
        if (context.Plan == null || context.Plazo == null || context.Periodicidad == null)
        {
            throw new Exception("Datos de plan, plazo o periodicidad no encontrados.");
        }
        decimal montoPrestamo = context.MontoPrestamo;

        // Para otras agencias, ajustar por pensión si no hay pensión alimenticia
        if (context.MontoPensionHijos < 1)
        {
            montoPrestamo = montoPrestamo * (context.PorcentajePencion / 100);
        }

        // Calcular descuento mensual
        decimal tasaDescuento = (decimal)_calculoHelper.CalcularTasaPeriodica(
            context.InteresAnualIVA,
            context.Periodicidad.NroPagosAnio);

        decimal descuentoMensual = _calculoHelper.CalcularPagoPeriodico(
            montoPrestamo,
            tasaDescuento,
            context.Plazo.Plazo);

        // Ajustar por pensión alimenticia si existe
        if (context.MontoPensionHijos > 0)
        {
            decimal ajuste = context.MontoPensionHijos * (context.PorcentajePencion / 100);
            descuentoMensual = Math.Max(descuentoMensual - ajuste, 0);
        }

        return ConstruirOpcionBase(context, montoPrestamo, descuentoMensual);
    }
}

