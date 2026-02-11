using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;

public interface ICotizadorService
{

    Task<Result> CalcularCAT(int idCotizador);

}
