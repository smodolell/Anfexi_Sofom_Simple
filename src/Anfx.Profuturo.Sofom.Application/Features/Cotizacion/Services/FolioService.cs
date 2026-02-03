using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Services;

public class FolioService : IFolioService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<FolioService> _logger;

    public FolioService(
        IApplicationDbContext dbContext,
        ILogger<FolioService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> GenerarFolioAsync(int idCotizador, int idAgencia)
    {
        try
        {
            // 1. Obtener la inicial de la agencia
            var agencia = await _dbContext.COT_Agencia.SingleOrDefaultAsync(r=>r.IdAgencia == idAgencia);
            var inicial = "X"; // Valor por defecto

            if (agencia != null && !string.IsNullOrEmpty(agencia.Nombre))
            {
                inicial = agencia.Nombre.Substring(0, 1).ToUpper();
            }

            // 2. Generar el folio
            return GenerarFolioSimple(idCotizador, inicial);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando folio para cotizador {Id}", idCotizador);
            return $"ERR{idCotizador}"; // Folio de error
        }
    }

    private static string GenerarFolioSimple(int idCotizador, string inicial)
    {
        // Ejemplo: A000123
        var idTexto = idCotizador.ToString();
        var cerosNecesarios = 6 - idTexto.Length;
        var ceros = new string('0', Math.Max(0, cerosNecesarios));

        return $"{inicial}{ceros}{idTexto}";
    }
}



