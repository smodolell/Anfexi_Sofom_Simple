using Anfx.Profuturo.Domain.Entities;
using Anfx.Profuturo.Sofom.Application.Common.Dtos;
using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Anfx.Profuturo.Sofom.Infrastructure.Services;

internal class DatabaseService(ApplicationDbContext dbContext,ILogger<DatabaseService> logger) : IDatabaseService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<DatabaseService> _logger = logger;

    public async Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Procedures.usp_ProcesaSaldoInsolutoAsync(idContrato);
    }

    public async Task<List<usp_RC_IniciaReestructuracionIMSSResult>> usp_RC_IniciaReestructuracionIMSSAsync(int? idContrato, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Procedures.usp_RC_IniciaReestructuracionIMSSAsync(idContrato);
    }

    public async Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, CancellationToken cancellationToken = default)
    {
      return  await _dbContext.Procedures.usp_SaldoLiquidacionAsync(idContrato, fecha);
    }

    public int? GetIdCiaTelefonica(string ciaTelefonica)
    {
        return _dbContext.Database.SqlQuery<int?>($" SELECT IdCiaTelefonica FROM CompaniaTelefonica WHERE Descripcion='{ciaTelefonica}'").FirstOrDefault();
    }

    public int? GetIdDelegacionIMMS(string dependenciaIMSS)
    {
        return _dbContext.Database.SqlQuery<int?>($" SELECT CLAVE FROM dbo.OT_DelegacionIMMS WHERE Delegacion='{dependenciaIMSS}'").FirstOrDefault();
    }

    public async Task CreateOneClickUserAsync()
    {
        try
        {
            var result = _dbContext.Database
                                .SqlQuery<string>($"EXEC sp_CrearUsuarioOneClic")
                                .AsEnumerable()
                                .FirstOrDefault();

            if (!string.IsNullOrEmpty(result))
            {
                if (result.StartsWith("ERROR:"))
                {
                    throw new InvalidOperationException(result);
                }

                await Task.CompletedTask;
            }
            else
            {
                throw new Exception("Error desconocido: El procedimiento almacenado no devolvió un resultado.");
            }
        }
        catch (SqlException ex)
        {
            throw new Exception($"Error SQL al generar usuario: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error inesperado al generar usuario: {ex.Message}");
        }
    }

    public async Task<int> GetOrCreateColoniaIdAsync(
       string? colonia,
       string? municipio,
       string? estado,
       string? codigoPostal,
       string? ciudad,
       string? pais)
    {
        try
        {
            _logger.LogInformation("Buscando/creando colonia: {@Datos}",
                new
                {
                    Colonia = colonia,
                    Municipio = municipio,
                    Estado = estado,
                    CodigoPostal = codigoPostal,
                    Ciudad = ciudad,
                    Pais = pais
                });

            var parameters = new SqlParameter[]
            {
                    new SqlParameter("@Colonia", colonia ?? (object)DBNull.Value),
                    new SqlParameter("@Municipio", municipio ?? (object)DBNull.Value),
                    new SqlParameter("@Estado", estado ?? (object)DBNull.Value),
                    new SqlParameter("@CodigoPostal", codigoPostal ?? (object)DBNull.Value),
                    new SqlParameter("@Ciudad", ciudad ?? (object)DBNull.Value),
                    new SqlParameter("@Pais", pais ?? (object)DBNull.Value)
            };

            var result = await _dbContext.Database
                .SqlQueryRaw<int>("EXEC dbo.GetOrCreateColoniaId_Transaccional @Colonia, @Municipio, @Estado, @CodigoPostal, @Ciudad, @Pais", parameters)
                .FirstOrDefaultAsync();

            if (result == 0 && !string.IsNullOrEmpty(colonia) &&
                !string.IsNullOrEmpty(municipio) && !string.IsNullOrEmpty(estado) &&
                !string.IsNullOrEmpty(codigoPostal))
            {
                _logger.LogWarning("No se pudo obtener/crear colonia para los datos proporcionados");
                throw new InvalidOperationException("El procedimiento almacenado no devolvió un IdColonia válido.");
            }

            _logger.LogInformation("Colonia encontrada/creada con ID: {ColoniaId}", result);
            return result;
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Error SQL al obtener/crear IdColonia");
            throw new ApplicationException("Error al acceder a la base de datos para obtener/crear colonia", ex);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error de operación al obtener/crear IdColonia");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener/crear IdColonia");
            throw new ApplicationException("Error inesperado al procesar la colonia", ex);
        }
    }

    public List<DocumentoConfigItem> ObtenerDocumentosConfigurados(int expedienteId, int idUsuario, int idAgencia)
    {
        var sql = @"
            SELECT
                ROW_NUMBER() OVER (ORDER BY ee.IdExpediente) AS NumeroFila,
                ee.IdExpediente,
                edc.IdDocumentoConfig,
                @p1 AS IdUsuario,
                eed.IdEstadoDocumento
            FROM EXP_Expediente ee
            INNER JOIN EXP_QuePersona_TPA eqpt ON eqpt.IdQuePersona = ee.IdQuePersona
            INNER JOIN EXP_DocumentoConfig edc ON edc.IdTipoPersonaAplica = eqpt.IdTipoPersonaAplica AND edc.Activo = 1
            INNER JOIN EXP_Documento ed ON ed.IdDocumento = edc.IdDocumento
            INNER JOIN EXP_EstadoDocumento eed ON eed.PorDefecto = 1
            WHERE ee.IdExpediente = @p0 
            AND NOT EXISTS(
                SELECT 1 FROM EXP_ExpedienteDocumento e 
                WHERE e.IdDocumentoConfig = edc.IdDocumentoConfig
                AND e.IdExpediente = @p0 
            ) 
            AND ed.IdAgencia = @p2";

        return _dbContext.Database
            .SqlQueryRaw<DocumentoConfigItem>(sql, expedienteId, idUsuario, idAgencia)
            .ToList();
    }

}

