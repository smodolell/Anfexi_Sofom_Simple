using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Steps;

class StepCrearSolicitud(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
    ILogger<StepCrearSolicitud> logger
) : ISagaStep<ConfirmarCotizacionContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly ILogger<StepCrearSolicitud> _logger = logger;

    public Task<Result> CompensateAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(ConfirmarCotizacionContext context, CancellationToken cancellationToken = default)
    {


        // Validar cotizador
        _logger.LogDebug("Buscando cotizador ID: {IdCotizador}", context.IdCotizador);

        var cotizador = await _dbContext.COT_Cotizador
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.IdCotizador == context.IdCotizador);
        if (cotizador == null) return Result.NotFound("Cotizador no existe");
        var idCotizador = cotizador.IdCotizador;

        if (cotizador == null)
        {
            _logger.LogWarning("Cotizador no encontrado. ID: {IdCotizador}", idCotizador);
            return Result.Error($"No se encontró el cotizador con ID {idCotizador}");
        }

        _logger.LogInformation("Cotizador encontrado. ID: {IdCotizador}, Nombre: {NombreCliente}",
            cotizador.IdCotizador, cotizador.NombreCliente ?? "N/A");


        // Iniciar transacción
        _logger.LogDebug("Iniciando transacción para creación de solicitud");
        try
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
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

                    solicitud.IdSolicitanteNavigation = newSolicitante;
                    _logger.LogDebug("Solicitante asignado a solicitud ID: {IdSolicitud}", solicitud.IdSolicitud);
                }
                else
                {
                    _logger.LogDebug("Solicitante existente encontrado. ID: {IdSolicitante}", solicitante.IdSolicitante);
                }

                // Guardar fase de solicitud
                _logger.LogDebug("Guardando fase SOL para solicitud ID: {IdSolicitud}", solicitud.IdSolicitud);
                await SaveFaseSolicitudAsync(solicitud);
                _logger.LogDebug("Fase SOL guardada exitosamente");

                
                _logger.LogInformation(
                    "✅ Proceso completado exitosamente. Solicitud creada: ID {IdSolicitud} para cotizador ID: {IdCotizador}",
                    solicitud.IdSolicitud, idCotizador);

                context.IdSolicitud = solicitud.IdSolicitud;
                
            });

            return Result.SuccessWithMessage("Solicitud Creada");
        }
        catch (Exception ex)
        {
            return Result.CriticalError($"Error al crear solicitud: {ex.Message}");
        }
        finally
        {
            _logger.LogDebug("Finalizada ejecución de CreateSolicitudAsync para cotizador ID: {IdCotizador}", context.IdCotizador);
        }
        
       
    }

    private async Task SaveFaseSolicitudAsync(OT_Solicitud solicitud)
    {
        var consecutivoFaseHistoria = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_FaseHistoria");

        if (!consecutivoFaseHistoria.Success) throw new Exception("NO se pudo generar el consecutivo de OT_FaseHistoria");

        var idAsesor = solicitud.IdAsesor;
        var fase = await _dbContext.OT_Fase
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ClaveUnica == "SOL") ?? throw new Exception("No se encontro la configuracion de fase SOL");
        var faseHistoria = new OT_FaseHistoria
        {
            IdFaseHistoria = consecutivoFaseHistoria.ConsecutivoGenerado,
            IdFase = fase.IdFase,
            IdSolicitud = solicitud.IdSolicitud,
            IdEstatusFase = 1,
            FechaEntrada = DateTime.Now,
            FechaUltimaModificacion = DateTime.Now,
            Comentario = "Prestamo por One Click",
            Activo = true,
            IdUsuario = idAsesor
        };


        await _dbContext.OT_FaseHistoria.AddAsync(faseHistoria);
    }
}
