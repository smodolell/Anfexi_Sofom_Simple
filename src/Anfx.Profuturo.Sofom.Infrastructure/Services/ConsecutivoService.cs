using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Anfx.Profuturo.Sofom.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de consecutivos
/// Historia Técnica: TEC-002 - Transacciones Serializables para Consecutivos
/// Basado en: OT_DocumentoModel.cs:109
/// </summary>
public class ConsecutivoService : IConsecutivoService
{
    private readonly ApplicationDbContext _context;

    public ConsecutivoService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene el siguiente consecutivo usando transacciones serializables
    /// Regla de Negocio: La generación de IDs debe usar transacciones serializables con bloqueo de tabla
    /// </summary>
    public async Task<(bool Success, int ConsecutivoGenerado, string ErrorMessage)> ObtenerSiguienteConsecutivoAsync(
        string nombreTabla,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Usar TransactionScope con IsolationLevel.Serializable
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable,
                    Timeout = TimeSpan.FromSeconds(30)
                },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                // Bloquear tabla Consecutivo con tablockx (simulado con FromSqlRaw)
                // En EF Core, el bloqueo se logra con la query y el nivel de aislamiento
                var consecutivo = await _context.Database
                    .ExecuteSqlRawAsync(
                        "SELECT TOP 1 * FROM cat.Consecutivo WITH (TABLOCKX) WHERE NombreTabla = {0}",
                        cancellationToken,
                        nombreTabla);

                // Buscar el consecutivo
                var consecutivoEntity = await _context.Set<Consecutivo>()
                    .FirstOrDefaultAsync(c => c.NombreTabla == nombreTabla, cancellationToken);

                int nuevoConsecutivo;

                if (consecutivoEntity != null)
                {
                    // Incrementar consecutivo existente
                    consecutivoEntity.IdConsecutivo++;
                    consecutivoEntity.FecUltimoCambio = DateTime.Now;
                    nuevoConsecutivo = consecutivoEntity.IdConsecutivo;
                }
                else
                {
                    // Crear nuevo consecutivo
                    consecutivoEntity = new Consecutivo
                    {
                        NombreTabla = nombreTabla,
                        IdConsecutivo = 1,
                        FecUltimoCambio = DateTime.Now
                    };
                    _context.Set<Consecutivo>().Add(consecutivoEntity);
                    nuevoConsecutivo = 1;
                }

                await _context.SaveChangesAsync(cancellationToken);

                // Completar transacción
                transactionScope.Complete();

                return (true, nuevoConsecutivo, string.Empty);
            }
        }
        catch (Exception ex)
        {
            return (false, 0, $"Error al generar consecutivo: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene el consecutivo actual sin incrementarlo
    /// </summary>
    public async Task<int> ObtenerConsecutivoActualAsync(
        string nombreTabla,
        CancellationToken cancellationToken = default)
    {
        var consecutivo = await _context.Set<Consecutivo>()
            .FirstOrDefaultAsync(c => c.NombreTabla == nombreTabla, cancellationToken);

        return consecutivo?.IdConsecutivo ?? 0;
    }

    /// <summary>
    /// Inicializa un consecutivo para una tabla si no existe
    /// </summary>
    public async Task<bool> InicializarConsecutivoAsync(
        string nombreTabla,
        int valorInicial = 1,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var existe = await _context.Set<Consecutivo>()
                .AnyAsync(c => c.NombreTabla == nombreTabla, cancellationToken);

            if (!existe)
            {
                var consecutivo = new Consecutivo
                {
                    NombreTabla = nombreTabla,
                    IdConsecutivo = valorInicial,
                    FecUltimoCambio = DateTime.Now
                };

                _context.Set<Consecutivo>().Add(consecutivo);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Reinicia el consecutivo de una tabla (usar con precaución)
    /// </summary>
    public async Task<bool> ReiniciarConsecutivoAsync(
        string nombreTabla,
        int nuevoValor = 1,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var consecutivo = await _context.Set<Consecutivo>()
                .FirstOrDefaultAsync(c => c.NombreTabla == nombreTabla, cancellationToken);

            if (consecutivo != null)
            {
                consecutivo.IdConsecutivo = nuevoValor;
                consecutivo.FecUltimoCambio = DateTime.Now;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
