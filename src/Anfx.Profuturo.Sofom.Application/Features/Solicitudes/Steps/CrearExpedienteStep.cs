using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;
using Microsoft.EntityFrameworkCore;

public class CrearExpedienteStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IDatabaseService databaseService,
    IConsecutivoService consecutivoService
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseService _databaseService = databaseService;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public Task<Result> CompensateAsync(CreateSolicitudContext context)
    {
        // Lógica de compensación si es necesario
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context)
    {
        try
        {
            // Iniciar UNA sola transacción
            await _unitOfWork.BeginTransactionAsync();

            var solicitud = await _dbContext.OT_Solicitud
                .Include(i => i.IdSolicitanteNavigation)
                .Include(i => i.IdCotizadorNavigation)
                .SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);

            if (solicitud == null)
                return Result.NotFound("No existe solicitud");

            var solictante = solicitud.IdSolicitanteNavigation;
            if (solictante == null)
                return Result.NotFound("No existe solicitante");

            var cotizador = solicitud.IdCotizadorNavigation;
            if (cotizador == null)
                return Result.NotFound("No existe cotizador");

            // 1. Crear expediente
            var expediente = await CrearExpedienteInternal(solictante.IdSolicitante, 2);

            // 2. Obtener usuario
            var idUsuario = await GetIdUsuario(context.Model.NumeroEmpleadoVendedor);
            idUsuario = idUsuario == 0 ? 493 : idUsuario;

            // 3. Crear documentos (dentro de la misma transacción)
            await CrearDocumentos(solicitud, expediente.IdExpediente, idUsuario);

            // Commit de TODO
            await _unitOfWork.CommitTransactionAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error($"Error al crear expediente: {ex.Message}");
        }
    }

    private async Task<EXP_Expediente> CrearExpedienteInternal(
        int idDuenioExpediente,
        int idQuePersona)
    {
        // YA NO inicia transacción aquí
        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("EXP_Expediente");

        if (!consecutivo.Success)
            throw new Exception($"Error al obtener consecutivo: {consecutivo.ErrorMessage}");

        var expediente = new EXP_Expediente
        {
            IdExpediente = consecutivo.ConsecutivoGenerado,
            IdDuenioExpediente = idDuenioExpediente,
            IdQuePersona = idQuePersona
        };

        _dbContext.EXP_Expediente.Add(expediente);
        await _dbContext.SaveChangesAsync(); // Guarda pero no commit

        return expediente;
    }

    private async Task<int> GetIdUsuario(string numeroEmpleadoVendedor)
    {
        if (string.IsNullOrEmpty(numeroEmpleadoVendedor))
            return 493;

        var usuarioInfo = await _dbContext.Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.NumeroEmpleado == numeroEmpleadoVendedor);

        return usuarioInfo?.IdUsuario ?? 493;
    }

    private async Task CrearDocumentos(
        OT_Solicitud solicitud,
        int currentIdExpediente,
        int idUsuarioActual)
    {
        // YA NO inicia transacción aquí

        var idAgencia = solicitud.IdCotizadorNavigation?.IdAgencia ?? 0;
        if (idAgencia == 0)
            throw new Exception("No se pudo obtener la agencia del cotizador");

        var documentosConfig = _databaseService.ObtenerDocumentosConfigurados(
            currentIdExpediente,
            idUsuarioActual,
            idAgencia);

        if (!documentosConfig.Any())
            return;

        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync(
            "EXP_ExpedienteDocumento"
        );

        if (!consecutivo.Success)
            throw new Exception($"Error al obtener consecutivo documentos: {consecutivo.ErrorMessage}");

        var idExpDocBase = consecutivo.ConsecutivoGenerado;

        var documentosAAgregar = documentosConfig.Select((s, index) => new EXP_ExpedienteDocumento
        {
            IdExpedienteDocumento = idExpDocBase + index, // Corregido: sin +1
            IdExpediente = s.IdExpediente,
            IdDocumentoConfig = s.IdDocumentoConfig,
            IdUsuario = s.IdUsuario,
            IdEstadoDocumento = s.IdEstadoDocumento,
            Comentario = s.Comentario ?? string.Empty,
            FechaUltimaModificacion = DateTime.Now,
        }).ToList();

        _dbContext.EXP_ExpedienteDocumento.AddRange(documentosAAgregar);
        await _dbContext.SaveChangesAsync(); // Guarda pero no commit

        var idExpedienteDocumentoMax = documentosAAgregar.Max(doc => doc.IdExpedienteDocumento);

        await _consecutivoService.ReiniciarConsecutivoAsync(
            "EXP_ExpedienteDocumento",
            idExpedienteDocumentoMax
        );
    }
}