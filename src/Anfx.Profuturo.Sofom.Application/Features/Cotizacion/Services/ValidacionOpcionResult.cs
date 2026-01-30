using Anfx.Profuturo.Sofom.Application.Common.Dtos;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class ValidacionOpcionResult
{
    public bool EsValida { get; set; }
    public bool EsMontoDepositoValido { get; set; }
    public RangoFecha? RangoValido { get; set; }
    public RangoFecha? RangoAproximado { get; set; }
    public bool EsMenor { get; set; }
    public bool EsMayor { get; set; }
    public bool EsDescuentoValido { get; set; }
    public List<ErrorDto> Errores { get; set; } = new();
}

