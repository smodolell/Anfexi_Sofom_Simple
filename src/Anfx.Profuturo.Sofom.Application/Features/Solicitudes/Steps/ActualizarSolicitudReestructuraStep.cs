using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class ActualizarSolicitudReestructuraStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public Task<Result> CompensateAsync(CreateSolicitudContext context)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context)
    {
        var model = context.Model;

        var solicitud = await _dbContext.OT_Solicitud
            .Include(i => i.IdCotizadorNavigation)
            .SingleOrDefaultAsync(s => s.IdSolicitud == context.IdSolicitud);

        if (solicitud == null)
            return Result.Error("Solicitud no encontrada");

        var usuario = await _dbContext.Usuario
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.NumeroEmpleado == model.NumeroEmpleadoVendedor);
        if (usuario == null)
            return Result.Error("Usuario no encontrada");

        
        var solicitante = await _dbContext.OT_Solicitante
          .Include(i => i.OT_Solicitante_InfoPension)
          .SingleOrDefaultAsync(r => r.IdSolicitante == (solicitud.IdSolicitante ?? 0));

        if (solicitante == null)
            return Result.Error("Solicitante no encontrada");


        await _unitOfWork.BeginTransactionAsync();


        try
        {


            int IdEstatusSolicitud = 1;
            int IdPersonaJuridica = 1;

            solicitud.IdEstatusSolicitud = IdEstatusSolicitud;
            solicitud.IdPersonaJuridica = IdPersonaJuridica;
            solicitud.FechaFirmaContrato = DateTime.Now;// FechaFirmaContrato;
            solicitud.FechaPrimeraRenta = DateTime.Now;//FechaPrimeraRenta;
            solicitud.IdAsesor = usuario.IdUsuario;
            solicitud.Reestructurado = true;
            solicitud.Activo = true;
            solicitud.FechaAlta = DateTime.Now;
            solicitud.EsImportada = false;

            var oCotizador = solicitud.IdCotizadorNavigation;
            if (oCotizador.IdAgencia == 3)
            {
                var infoPension = solicitante.OT_Solicitante_InfoPension.FirstOrDefault();
                if (infoPension == null)
                {
                    infoPension = new OT_Solicitante_InfoPension
                    {
                        IdSolicitante = solicitante.IdSolicitante,
                    };
                    solicitante.OT_Solicitante_InfoPension.Add(infoPension);
                }

                infoPension.NumeroEmpleado = model.Persona.NumeroEmpleado;
                infoPension.MontoPension = model.Persona.MontoPension;
                infoPension.FechaPagoPension = Convert.ToDateTime(model.Persona.FechaPagoPension);
                infoPension.AñosAntiguedad = model.Persona.AñosAntiguedad;
                infoPension.NumeroOferta = model.Persona.NumeroOferta;
                infoPension.FolioIdentificador = model.Persona.FolioTramite;
            }
            _dbContext.OT_Solicitante.Update(solicitante);

            await _unitOfWork.CommitTransactionAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error(ex.Message);

        }

    }
}
