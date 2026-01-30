using Anfx.Profuturo.Sofom.Application.Common.Dtos;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class OpcionValidator : IOpcionValidator
{
    private readonly IApplicationDbContext _dbContext;

    public OpcionValidator(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ValidacionOpcionResult> ValidarOpcion(
        CotizadorOpcionDto opcion,
        CalculoContextDto context)
    {
        var resultado = new ValidacionOpcionResult();

        // 1. Validar monto de depósito
        resultado.EsMontoDepositoValido = await ValidarMontoDeposito(opcion);

        // 2. Validar rangos de edad
        var validacionRango = await ValidarRangoEdad(
            context.EdadSolicitante,
            opcion.MontoPrestamo,
            context.IdPlan);

        resultado.RangoValido = validacionRango.RangoValido;
        resultado.RangoAproximado = validacionRango.RangoAproximado;

        // 3. Validar límites del plan
        var validacionLimites = await ValidarLimitesPlan(opcion, context.Plan);
        resultado.EsMenor = validacionLimites.EsMenor;
        resultado.EsMayor = validacionLimites.EsMayor;
        resultado.Errores.AddRange(validacionLimites.Errores);

        // 4. Validar porcentaje de descuento
        var validacionDescuento = await ValidarPorcentajeDescuento(
            opcion,
            context.IngresosMensuales,
            context.Plan);
        resultado.EsDescuentoValido = validacionDescuento.EsValido;
        resultado.Errores.AddRange(validacionDescuento.Errores);

        // 5. Validar si la opción es completamente válida
        resultado.EsValida = resultado.EsMontoDepositoValido &&
                            resultado.RangoValido != null &&
                            !resultado.EsMenor &&
                            !resultado.EsMayor &&
                            resultado.EsDescuentoValido &&
                            resultado.Errores.Count == 0;

        return resultado;
    }

    private async Task<bool> ValidarMontoDeposito(CotizadorOpcionDto opcion)
    {
        return Math.Round(opcion.MontoAproximadoDeposito, 3) > 0;
    }

    private async Task<(RangoFecha? RangoValido, RangoFecha? RangoAproximado)> ValidarRangoEdad(
        int edadSolicitante,
        decimal montoPrestamo,
        int idPlan)
    {
        // Buscar rango exacto
        var rangoValido = await _dbContext.RangoFecha
            .FirstOrDefaultAsync(f =>
                edadSolicitante >= f.EdadMinima &&
                edadSolicitante <= f.EdadMaxima &&
                montoPrestamo <= f.MontoPrestamo &&
                f.IdCOT_Plan == idPlan);

        // Buscar rango aproximado si no hay exacto
        RangoFecha? rangoAproximado = null;
        if (rangoValido == null)
        {
            rangoAproximado = await _dbContext.RangoFecha
                .Where(f => f.IdCOT_Plan == idPlan)
                .AsAsyncEnumerable()
                .OrderBy(f => Math.Min(
                    Math.Abs(edadSolicitante - f.EdadMinima.GetValueOrDefault()),
                    Math.Abs(edadSolicitante - f.EdadMaxima.GetValueOrDefault())))
                .FirstOrDefaultAsync();
        }

        return (rangoValido, rangoAproximado);
    }
    private async Task<(bool EsMenor, bool EsMayor, List<ErrorDto> Errores)> ValidarLimitesPlan(
        CotizadorOpcionDto opcion,
        COT_Plan plan)
    {
        var errores = new List<ErrorDto>();
        bool esMenor = false;
        bool esMayor = false;

        // Validar mínimo
        if (Math.Round(opcion.MontoPrestamo, 3) < plan.ImporteMinimo)
        {
            esMenor = true;
            errores.Add(CrearErrorMontoMinimo(opcion.Plazo, plan.ImporteMinimo));
        }

        // Validar máximo
        if (plan.ImporteMaximo < Math.Round(opcion.MontoPrestamo, 3))
        {
            esMayor = true;
            errores.Add(CrearErrorMontoMaximo(opcion.Plazo, plan.ImporteMaximo));
        }

        return (esMenor, esMayor, errores);
    }

    private async Task<(bool EsValido, List<ErrorDto> Errores)> ValidarPorcentajeDescuento(
        CotizadorOpcionDto opcion,
        decimal? ingresosMensuales,
        COT_Plan plan)
    {
        var errores = new List<ErrorDto>();

        if (ingresosMensuales == null || opcion.TablaAmortiza?.Count == 0)
            return (false, errores);

        var montoDescuento = (ingresosMensuales * (plan.porcentajeDescuento / 100.00m));
        montoDescuento = Math.Round(montoDescuento ?? 0.00m, 3);

        if (Math.Round(opcion.TablaAmortiza[0].Total, 3) > montoDescuento)
        {
            errores.Add(CrearErrorPorcentajeDescuento(opcion.Plazo, plan.porcentajeDescuento.ToString()));
            return (false, errores);
        }

        return (true, errores);
    }

    private ErrorDto CrearErrorMontoMinimo(int plazo, decimal? importeMinimo)
    {
        // Implementar según tu servicio de errores
        return new ErrorDto
        {
            Codigo = "1005",
            Mensaje = $"PARA EL PLAZO {plazo}, EL MONTO MÍNIMO PERMITIDO ES {importeMinimo.GetValueOrDefault().ToString("C", new CultureInfo("es-MX"))}"
        };
    }

    private ErrorDto CrearErrorMontoMaximo(int plazo, decimal? importeMaximo)
    {
        return new ErrorDto
        {
            Codigo = "1007",
            Mensaje = $"PARA EL PLAZO {plazo}, EL MONTO MÁXIMO PERMITIDO ES {importeMaximo.GetValueOrDefault().ToString("C", new CultureInfo("es-MX"))}"
        };
    }

    private ErrorDto CrearErrorPorcentajeDescuento(int plazo, string porcentaje)
    {
        return new ErrorDto
        {
            Codigo = "1004",
            Mensaje = $"PARA EL PLAZO {plazo}, EL PORCENTAJE DE DESCUENTO PERMITIDO ES {porcentaje}%"
        };
    }
}

