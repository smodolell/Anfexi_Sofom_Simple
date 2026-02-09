using Anfx.Profuturo.Sofom.Application.Common.Utils;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class CotizadorService(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
    IFolioService _folioService

) : ICotizadorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly IFolioService _folioService = _folioService;

    public async Task<Result> CalcularCAT(int idCotizador)
    {
        try
        {
            var cotizador = await _dbContext.COT_Cotizador
                .Include(i => i.COT_TablaAmortiza)
                .SingleOrDefaultAsync(r => r.IdCotizador == idCotizador);

            if (cotizador == null)
                return Result.Error("Cotizador no encontrado");

            var tablaAmortizacion = cotizador.COT_TablaAmortiza.ToList();
            var montos = new decimal[Convert.ToInt32(cotizador.NumeroPagosFijos)];

            for (int i = 0; i < montos.Length && i < tablaAmortizacion.Count; i++)
            {
                montos[i] = tablaAmortizacion[i].Total ?? 0;
            }

            // 2. Calcular CAT usando la clase CAT
            var calculadorCAT = new CAT(
                cotizador.MontoSolicitar,
                 0,
                Convert.ToInt32(cotizador.NumeroPagosFijos),
                montos,
                cotizador.FrecuenciaPago ?? 3
            );

            // 3. Obtener resultado
            var catCalculado = calculadorCAT.CalculaCAT();

            if (catCalculado < 0) // La clase retorna -1.00 en caso de error
            {
                return Result.Error("No se pudo calcular el CAT");
            }


            var catPorcentaje = Math.Round((decimal)catCalculado * 100, 1);

            await _unitOfWork.BeginTransactionAsync();


            cotizador.CAT = catPorcentaje;
            cotizador.CalculoCAT = catPorcentaje;


            _dbContext.COT_Cotizador.Update(cotizador);

            await _unitOfWork.CommitTransactionAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
          await   _unitOfWork.RollbackTransactionAsync();
            return Result.Error($"Error al calcular CAT: {ex.Message}");
        }
    }

}