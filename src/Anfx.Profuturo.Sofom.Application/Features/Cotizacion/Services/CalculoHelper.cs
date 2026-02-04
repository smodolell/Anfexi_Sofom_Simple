using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class CalculoHelper(IApplicationDbContext dbContext,IDatabaseService proceduresServices) : ICalculoHelper
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseService _proceduresServices = proceduresServices;

    public decimal CalcularValorPresente(decimal pagoPeriodico, decimal tasaPeriodica, int numeroPeriodos)
    {
        double pago = (double)pagoPeriodico;
        double tasa = (double)tasaPeriodica;

        var numerador = Math.Pow(1 + tasa, numeroPeriodos) - 1;
        var denominador = tasa * Math.Pow(1 + tasa, numeroPeriodos);
        var valorPresente = pago * (numerador / denominador);

        return (decimal)valorPresente;
    }

    public decimal CalcularPagoPeriodico(decimal valorPresente, decimal tasaPeriodica, int numeroPeriodos)
    {
        double vp = (double)valorPresente;
        double tasa = (double)tasaPeriodica;

        var pago = Financial.Pmt(tasa, -numeroPeriodos, vp, 0);
        return (decimal)pago;
    }

    public decimal CalcularTasaConIVA(decimal tasaBase, decimal porcentajeIVA = 16)
    {
        return (tasaBase / 100) * (1 + (porcentajeIVA / 100));
    }

    public double CalcularTasaPeriodica(decimal tasaAnualIVA, int nroPagosAnio)
    {
        return (double)((double)tasaAnualIVA / nroPagosAnio);
    }

    public decimal CalcularDescuentoConPensionAlimenticia(decimal capacidadPago, decimal montoPensionAlimenticia, decimal porcentajePension)
    {
        if (montoPensionAlimenticia > 0)
            return capacidadPago - (montoPensionAlimenticia * (porcentajePension / 100));

        return capacidadPago * (porcentajePension / 100);
    }

    public decimal AplicarLimitesMonto(decimal montoCalculado, decimal? montoMinimo, decimal? montoMaximo)
    {
        if (montoMinimo.HasValue && montoCalculado < montoMinimo.Value)
            return montoMinimo.Value;

        if (montoMaximo.HasValue && montoCalculado > montoMaximo.Value)
            return montoMaximo.Value;

        return montoCalculado;
    }

    public decimal CalcularDescuentoMaximoPermitido(decimal ingresosMensuales, decimal? montoMinimoPension, decimal porcentajeDescuento, int? noPagosMes)
    {
        decimal montoMensual = ingresosMensuales;
        decimal descuentoPorPorcentaje = montoMensual * (porcentajeDescuento / 100);

        if (montoMinimoPension.HasValue && noPagosMes.HasValue && noPagosMes.Value > 0)
        {
            decimal descuentoPorMinimo = montoMensual - (montoMinimoPension.Value / noPagosMes.Value);
            return Math.Min(descuentoPorPorcentaje, descuentoPorMinimo);
        }

        return descuentoPorPorcentaje;
    }

    public decimal CalcularSumaFlujosDescontados(decimal flujoPeriodico, decimal tasaDescuento, int numeroPeriodos)
    {
        decimal suma = 0;

        for (int i = 1; i <= numeroPeriodos; i++)
        {
            var factorDescuento = Math.Pow(1 + (double)tasaDescuento, i);
            suma += (decimal)((double)flujoPeriodico / factorDescuento);
        }

        return suma;
    }

    public decimal CalcularSeguroPiramidado(decimal montoBase, decimal porcentajeSeguro)
    {
        return montoBase * (1 + (1000 * (porcentajeSeguro / 100)) /
            (1000 - ((porcentajeSeguro / 100) * 1000)));
    }

    public int CalcularNumeroPagos(int plazoAnios, int nroPagosAnio)
    {
        return (plazoAnios / 12) * nroPagosAnio;
    }

    public async Task<decimal> CalcularSaldos(bool esReestructura, string contratos)
    {

        if (!esReestructura)
        {
            return 0;
        }

        var listacontratos = contratos.Split('_');
        decimal suma = 0;

        foreach (var item in listacontratos)
        {
            var oContrato = await _dbContext.Contrato.FirstOrDefaultAsync(r => r.Contrato1 == item);
            if (oContrato == null) throw new Exception("CONTRATO DE RESTRUCTURA NO ENCONTRADO");

            var id = oContrato.IdContrato;
            var fechaActual = DateTime.Now;
            var resultado = await _proceduresServices.usp_SaldoLiquidacionAsync(id, fechaActual);
            var first = resultado.FirstOrDefault();
            suma = suma + (first == null ? 0 : first.Saldo ?? 0);
        }

        return suma;
    }
}