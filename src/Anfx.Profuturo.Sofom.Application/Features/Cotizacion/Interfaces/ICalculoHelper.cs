namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface ICalculoHelper
{
    decimal CalcularValorPresente(decimal pagoPeriodico, decimal tasaPeriodica, int numeroPeriodos);
    decimal CalcularPagoPeriodico(decimal valorPresente, decimal tasaPeriodica, int numeroPeriodos);
    decimal CalcularTasaConIVA(decimal tasaBase, decimal porcentajeIVA = 16);
    double CalcularTasaPeriodica(decimal tasaAnualIVA, int nroPagosAnio);
    decimal CalcularDescuentoConPensionAlimenticia(decimal capacidadPago, decimal montoPensionAlimenticia, decimal porcentajePension);
    decimal AplicarLimitesMonto(decimal montoCalculado, decimal? montoMinimo, decimal? montoMaximo);
    decimal CalcularDescuentoMaximoPermitido(decimal ingresosMensuales, decimal? montoMinimoPension, decimal porcentajeDescuento, int? noPagosMes);
    decimal CalcularSumaFlujosDescontados(decimal flujoPeriodico, decimal tasaDescuento, int numeroPeriodos);
    decimal CalcularSeguroPiramidado(decimal montoBase, decimal porcentajeSeguro);
    int CalcularNumeroPagos(int plazoAnios, int nroPagosAnio);


    Task<decimal> CalcularSaldos(bool esReestructura, string contratos);
}


