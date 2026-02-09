using Anfx.Profuturo.Sofom.Application.Common.Dtos;
using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Steps;

internal class StepGuardarDocumento(IUnitOfWork unitOfWork, IApplicationDbContext dbContext, IOptions<AppConfig> options) : ISagaStep<GuardarExpedienteContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly AppConfig _appConfig = options.Value;

    public async Task<Result> CompensateAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(GuardarExpedienteContext context, CancellationToken cancellationToken = default)
    {
        if (context.Documento != null && context.Documento.Length > 0)
        {

            var expediente = await _dbContext.EXP_Expediente.FirstOrDefaultAsync(w => w.IdExpediente == context.IdExpediente);

            if (expediente == null)
                return Result.NotFound("No existe EXP_DocumentoConfig");


            var documentoConfig = _dbContext.EXP_DocumentoConfig.FirstOrDefault(r => r.IdDocumento == context.IdTipoDocumento
                  && r.Activo
                  && r.IdTipoPersonaAplica == 3);

            if (documentoConfig == null)
            {
                return Result.NotFound("No existe EXP_DocumentoConfig");
            }

            var expedienteDocumento = _dbContext.EXP_ExpedienteDocumento
                    .FirstOrDefault(r => r.IdExpediente == context.IdExpediente
                    && r.IdDocumentoConfig == documentoConfig.IdDocumentoConfig);


            if (expedienteDocumento == null)
            {
                return Result.NotFound("No existe EXP_DocumentoConfig");
            }


            var id_documento = documentoConfig.IdDocumento;
            var exp_doc = await _dbContext.EXP_Documento.FirstOrDefaultAsync(w => w.IdDocumento == id_documento);


            var nombreArchivo = exp_doc != null ? exp_doc.NombreArchivo : "SinNombreDeArchivo";
            var idSolicitante = expediente.IdDuenioExpediente;



            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var nombre = nombreArchivo + context.Folio + "_1";
                var extencion = GetFileExtension(context.MIME);
                var expedienteArchivo = new EXP_ExpedienteArchivo
                {
                    IdExpedienteDocumento = expedienteDocumento.IdExpedienteDocumento,
                    NombreUnico = Guid.NewGuid().ToString(),
                    NombreReal = nombre + extencion,
                    Extension = extencion.Split('.')[1],
                    FechaSubida = DateTime.Now,
                    ContentType = context.MIME,
                    ContentLength = context.Documento.Length
                };
                var path = _appConfig.DirectorioExpediente;

                var directorio = path + "/" + "/" + expedienteArchivo.NombreUnico;
                var dir = new FileInfo(directorio);
                if (dir.Directory != null && !dir.Directory.Exists)
                {
                    dir.Directory.Create();
                }

                File.WriteAllBytes(directorio, Convert.FromBase64String(context.Documento));

                await _dbContext.EXP_ExpedienteArchivo.AddAsync(expedienteArchivo);
                await _unitOfWork.CommitTransactionAsync();

                context.IdExpedienteDocumento = expedienteDocumento.IdExpedienteDocumento;

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Error($"Documento no valido:{ex.Message}");
            }
            
        }
        return Result.Error("Documento no valido");
    }

    string GetFileExtension(string mime)
    {
        if (string.IsNullOrWhiteSpace(mime))
        {
            throw new ArgumentException("El parámetro TipoMIME no puede estar vacío.", nameof(mime));
        }

        var data = _dbContext.CatTiposMimeDocumentos.FirstOrDefault(r => r.TipoMIME == mime);

        if (data != null)
        {
            return data.Extencion;
        }
        return string.Empty;

    }

}