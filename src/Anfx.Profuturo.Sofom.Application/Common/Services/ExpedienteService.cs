using Anfx.Profuturo.Sofom.Application.Common.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Common.Services;

public class ExpedienteService(IUnitOfWork unitOfWork, IApplicationDbContext dbContext, IConsecutivoService consecutivoService) : IExpedienteService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public async Task<int> CreateOrUpdateExpediente(int idDuenioExpediente, int idQuePersona, int idUsuario, int idAgencia)
    {
        int idUsuarioParaDocumentos = 493;

        var usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
        if (usuario != null)
        {
            idUsuarioParaDocumentos = idUsuario;
        }


        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var expediente = await _dbContext.EXP_Expediente
                .FirstOrDefaultAsync(r => r.IdDuenioExpediente == idDuenioExpediente && r.IdQuePersona == idQuePersona);

            if (expediente == null)
            {
                var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("EXP_Expediente");
                if (!consecutivo.Success)
                    throw new Exception("NO es posible generar el consecutivo de la tabla EXP_Expediente");

                expediente = new EXP_Expediente
                {
                    IdExpediente = consecutivo.ConsecutivoGenerado,
                    IdDuenioExpediente = idDuenioExpediente,
                    IdQuePersona = idQuePersona
                };
                await _dbContext.EXP_Expediente.AddAsync(expediente);
                await _dbContext.SaveChangesAsync();
            }



            var pIdExpediente = new SqlParameter("@p0", expediente.IdExpediente);
            var pIdUsuario = new SqlParameter("@p1", idUsuarioParaDocumentos);
            var pIdAgencia = new SqlParameter("@p2", idAgencia);

            string sqlQuery = $@"
                    SELECT
                        CAST((ROW_NUMBER() OVER (ORDER BY ee.IdExpediente)) AS INT) AS IdExpedienteDocumento, 
                        ee.IdExpediente,
                        edc.IdDocumentoConfig,
                        CAST(@p1 AS INT) AS IdUsuario, 
                        eed.IdEstadoDocumento,                                  
                        eed.Titulo AS EstadoDocumento,          
                        ed.NombreDocumento,                                     
                        eed.Icono,                                               
                        '' AS Grupo,                                               
                        CAST(NULL AS DATETIME) AS FechaUltimaModificacion,      
                        CAST(NULL AS DATETIME) AS FechaVigencia,                
                        CAST('' AS VARCHAR(100)) AS Comentario                  
                    FROM EXP_Expediente ee
                    INNER JOIN EXP_QuePersona_TPA eqpt ON eqpt.IdQuePersona = ee.IdQuePersona
                    INNER JOIN EXP_DocumentoConfig edc ON edc.IdTipoPersonaAplica = eqpt.IdTipoPersonaAplica AND edc.Activo = 1
                    INNER JOIN EXP_Documento ed ON ed.IdDocumento = edc.IdDocumento
                    INNER JOIN EXP_EstadoDocumento eed ON eed.PorDefecto = 1
                    WHERE ee.IdExpediente = @p0 
                    AND NOT EXISTS(
                        SELECT 1 FROM EXP_ExpedienteDocumento e WHERE e.IdDocumentoConfig = edc.IdDocumentoConfig
                        AND e.IdExpediente = @p0 
                    ) 
                    AND ed.IdAgencia = @p2";

            var itemsFromQuery = _dbContext.Database
                                         .SqlQueryRaw<DocumentoConfigItem>(sqlQuery, pIdExpediente, pIdUsuario, pIdAgencia)
                                         .ToList();


            if (!itemsFromQuery.Any())
            {

                var consecutivoExpedienteDocumento = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("EXP_ExpedienteDocumento");
                if (!consecutivoExpedienteDocumento.Success)
                    throw new Exception("NO es posible generar el consecutivo de la tabla EXP_ExpedienteDocumento");

                int idExpDocBase = consecutivoExpedienteDocumento.ConsecutivoGenerado;

                var documentosAAgregar = itemsFromQuery.Select((s, index) => new EXP_ExpedienteDocumento
                {
                    IdExpedienteDocumento = idExpDocBase + (index + 1),
                    IdExpediente = s.IdExpediente,
                    IdDocumentoConfig = s.IdDocumentoConfig,
                    IdUsuario = s.IdUsuario,
                    IdEstadoDocumento = s.IdEstadoDocumento,
                    Comentario = s.Comentario,
                    FechaUltimaModificacion = DateTime.Now,
                }).ToList();
                await _dbContext.EXP_ExpedienteDocumento.AddRangeAsync(documentosAAgregar);
                await _dbContext.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            return expediente.IdExpediente;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }



    }
}
