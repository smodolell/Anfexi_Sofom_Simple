using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Factories;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Commands;

public record CreateSolicitudCommand(SolicitudModel Model) : ICommand<Result<CreateSolicitudDto>>;

internal class CreateSolicitudCommandHandler(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IConsecutivoService consecutivoService,
     ISolicitudSagaFactory solicitudSagaFactory
) : ICommandHandler<CreateSolicitudCommand, Result<CreateSolicitudDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;
    private readonly ISolicitudSagaFactory _solicitudSagaFactory = solicitudSagaFactory;


    public async Task<Result<CreateSolicitudDto>> HandleAsync(CreateSolicitudCommand message, CancellationToken cancellationToken = default)
    {

        var model = message.Model;
        var cotizador = await _dbContext.COT_Cotizador
            .FirstOrDefaultAsync(r => r.Folio == model.Folio);
        if (cotizador == null)
        {
            return Result.NotFound("Cotizador no encotrado");
        }
        var solicitud = await _dbContext.OT_Solicitud
            .FirstOrDefaultAsync(r => r.IdCotizador == cotizador.IdCotizador);
        if (solicitud == null)
        {
            return Result.NotFound("Solicitud no encotrado");
        }

        var rees = await _dbContext.RC_Reestructuracion
            .Where(r => r.IdSolicitudNew == solicitud.IdSolicitud)
            .OrderByDescending(r => r.IdReestructuracion)
            .FirstOrDefaultAsync();


        
        var context = new CreateSolicitudContext
        {
            Folio = cotizador.Folio,
            IdSolicitante = solicitud.IdSolicitante ?? 0,
            EsReestructuracion = rees != null,
            Model = model,
        };



        var saga = _solicitudSagaFactory.CreateSolicitudSaga(context.EsReestructuracion);

        var sagaResult = await saga.ExecuteAsync(context);



        


        var result = await saga.ExecuteAsync(context);
        if (result.IsSuccess)
        {

        }
        return Result.Error();
    }
}









internal class CreateSolicitudDto
{
    public List<SolicitudDto> lista { get; set; }
    public string folio { get; set; }
}


public class SolicitudDto
{
    public string Folio { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string CURP { get; set; }
    public string NSS { get; set; }
    public string NumeroEmpleado { get; set; }
    public string estadoSolicitud { get; set; }
    public string estadoFase { get; set; }
    public string folioSuapSipre { get; set; }
    public string esReestructura { get; set; }
    [NotMapped]
    public List<FaseItem> HistoriaFases { get; set; }
}

public class FaseItem
{
    public string Fase { get; set; }
    public string EstadoFase { get; set; }
    public string fechaOperacion { get; set; }
    public string comentario { get; set; }
}
