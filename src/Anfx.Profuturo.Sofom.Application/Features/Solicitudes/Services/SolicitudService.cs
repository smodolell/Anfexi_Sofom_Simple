using Anfx.Profuturo.Sofom.Application.Features.Cotizacion;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Services;

public class SolicitudService(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
    ILogger<SolicitudService> logger
) : ISolicitudService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly ILogger<SolicitudService> _logger = logger;

    public async Task<Result<int>> CreateSolicitudAsync(int idCotizador)
    {
        // Log de inicio del proceso
        _logger.LogInformation("Iniciando creación de solicitud para cotizador ID: {IdCotizador}", idCotizador);

        try
        {
            // Validar cotizador
            _logger.LogDebug("Buscando cotizador ID: {IdCotizador}", idCotizador);
            var cotizador = await _dbContext.COT_Cotizador.SingleOrDefaultAsync(r => r.IdCotizador == idCotizador);

            if (cotizador == null)
            {
                _logger.LogWarning("Cotizador no encontrado. ID: {IdCotizador}", idCotizador);
                return Result.Error($"No se encontró el cotizador con ID {idCotizador}");
            }

            _logger.LogInformation("Cotizador encontrado. ID: {IdCotizador}, Nombre: {NombreCliente}",
                cotizador.IdCotizador, cotizador.NombreCliente ?? "N/A");

            // Iniciar transacción
            _logger.LogDebug("Iniciando transacción para creación de solicitud");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var consecutivoSolicitud = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_Solicitud");

                if (!consecutivoSolicitud.Success)
                {
                    throw new Exception("No se a podido crear el consecutivo de OT_Solicitud");
                }
                // Crear solicitud
                _logger.LogDebug("Creando entidad de solicitud");
                var solicitud = new OT_Solicitud
                {
                    IdSolicitud = consecutivoSolicitud.ConsecutivoGenerado,
                    IdCotizador = idCotizador,
                    IdEstatusSolicitud = 1,
                    IdAsesor = CotizacionConstants.USUARIO_DEFAULT_ASESOR,
                    IdPersonaJuridica = 1,
                    EsImportada = false,
                    FechaAlta = DateTime.Now
                };





                await _dbContext.OT_Solicitud.AddAsync(solicitud);
                _logger.LogInformation("Solicitud creada exitosamente. ID: {IdSolicitud}", solicitud.IdSolicitud);

                // Crear relación cotizador-solicitud
                _logger.LogDebug("Creando relación cotizador-solicitud. Cotizador: {IdCotizador}, Solicitud: {IdSolicitud}",
                    cotizador.IdCotizador, solicitud.IdSolicitud);

                var solicitudCotizador = new COT_SolicitudCotizador
                {
                    IdSolicitud = solicitud.IdSolicitud,
                    IdCotizador = cotizador.IdCotizador
                };
                await _dbContext.COT_SolicitudCotizador.AddAsync(solicitudCotizador);
                _logger.LogDebug("Relación cotizador-solicitud creada exitosamente");

                // Verificar y crear solicitante si no existe
                _logger.LogDebug("Verificando existencia de solicitante para solicitud ID: {IdSolicitud}", solicitud.IdSolicitud);
                var solicitante = await _dbContext.OT_Solicitante.SingleOrDefaultAsync(r => r.IdSolicitante == solicitud.IdSolicitud);

                if (solicitante == null)
                {
                    _logger.LogInformation("Solicitante no encontrado. Creando nuevo solicitante");

                    var consecutivoSolictante = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_Solicitante");

                    var newSolicitante = new OT_Solicitante
                    {
                        IdSolicitante = consecutivoSolictante.ConsecutivoGenerado,
                        Nombres_RazonSocial = "",
                        RFC = "",
                        FechaNacimiento = cotizador.FechaNacimiento ?? DateTime.Now.AddYears(-30),
                        IdPersonaJuridica = 0,
                        Email = ""
                    };

                    await _dbContext.OT_Solicitante.AddAsync(newSolicitante);
                    _logger.LogInformation("Solicitante creado exitosamente. ID: {IdSolicitante}", newSolicitante.IdSolicitante);

                    solicitud.Solicitante = newSolicitante;
                    _logger.LogDebug("Solicitante asignado a solicitud ID: {IdSolicitud}", solicitud.IdSolicitud);
                }
                else
                {
                    _logger.LogDebug("Solicitante existente encontrado. ID: {IdSolicitante}", solicitante.IdSolicitante);
                }

                // Guardar fase de solicitud
                _logger.LogDebug("Guardando fase SOL para solicitud ID: {IdSolicitud}", solicitud.IdSolicitud);
                await SaveFaseSolicitudAsync(solicitud.IdSolicitud, "SOL", false);
                _logger.LogDebug("Fase SOL guardada exitosamente");

                // Commit transacción
                _logger.LogDebug("Confirmando transacción");
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(
                    "✅ Proceso completado exitosamente. Solicitud creada: ID {IdSolicitud} para cotizador ID: {IdCotizador}",
                    solicitud.IdSolicitud, idCotizador);

                await _unitOfWork.CommitTransactionAsync();
                return Result.Created(solicitud.IdSolicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "❌ Error durante la creación de solicitud para cotizador ID: {IdCotizador}. Realizando rollback",
                    idCotizador);

                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogDebug("Rollback completado");

                // Puedes clasificar diferentes tipos de errores
                if (ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogCritical("Timeout en base de datos durante creación de solicitud");
                    return Result.CriticalError("Timeout en la operación. Por favor intente nuevamente.");
                }

                if (ex.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("Intento de crear registro duplicado");
                    return Result.Error("Ya existe una solicitud para este cotizador.");
                }

                return Result.CriticalError($"Error al crear solicitud: {ex.Message}");
            }
        }
        finally
        {
            // Log de finalización (opcional, útil para métricas de tiempo)
            _logger.LogDebug("Finalizada ejecución de CreateSolicitudAsync para cotizador ID: {IdCotizador}", idCotizador);
        }
    }

    public async Task<Result> GuardarFaseRees(int idsolicitud, string claveFase, bool completo)
    {

        var solicitud = await _dbContext.OT_Solicitud.SingleOrDefaultAsync(r => r.IdSolicitud == idsolicitud);
        if (solicitud == null)
        {
            return Result.NotFound("NO existe la solicitud");
        }
        var idAsesor = solicitud.IdAsesor;

        var proceso = await _dbContext.RC_Proceso.FirstOrDefaultAsync(r => r.ClaveProceso == claveFase);
        if (proceso == null)
        {
            return Result.NotFound("NO existe el Proceso");
        }
        var idProceso = proceso.IdProceso;

        var restructura = await _dbContext.RC_Reestructuracion.FirstOrDefaultAsync(r => r.IdSolicitudNew == idsolicitud);
        if (restructura == null)
        {
            return Result.NotFound("NO existe la restructura");
        }
        var idReestructura = restructura.IdReestructuracion;
        var procesoHistorial = new RC_ProcesoHistorial
        {
            IdReestructuracion = idReestructura,
            IdProceso = idProceso,
            IdEstadoProceso = completo ? 2 : 1,
            FechaRegistro = DateTime.Now,
            FechaUltimaModificacion = DateTime.Now,
            Comentarios = "Prestamo por One Click",
            EsProcesoActual = true,
            IdUsuario = idAsesor
        };

        _dbContext.RC_ProcesoHistorial.Add(procesoHistorial);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task SaveFaseSolicitudAsync(int idSolicitud, string clave, bool completo = true)
    {
        var solicitud = await _dbContext.OT_Solicitud.FirstOrDefaultAsync(r => r.IdSolicitud == idSolicitud);
        if (solicitud == null) throw new Exception("No se encontró la solicitud");

        var fase = _dbContext.OT_Fase.FirstOrDefault(r => r.ClaveUnica == clave);
        if (fase == null) throw new Exception("No se encontró la fase con la clave proporcionada");

        var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_FaseHistoria");
        if (!consecutivo.Success) throw new Exception("No se pudo obtener el consecutivo para OT_FaseHistoria");

        var idConsecutivo = consecutivo.ConsecutivoGenerado;


        var oFaseHistoria = new OT_FaseHistoria
        {
            IdFaseHistoria = idConsecutivo,
            IdFase = fase.IdFase,
            IdSolicitud = solicitud.IdSolicitud,
            IdEstatusFase = completo ? 4 : 1,
            FechaEntrada = DateTime.Now,
            FechaUltimaModificacion = DateTime.Now,
            Comentario = "Prestamo por One Click",
            Activo = true,
            IdUsuario = solicitud.IdAsesor
        };
        await _dbContext.OT_FaseHistoria.AddAsync(oFaseHistoria);

    }
}