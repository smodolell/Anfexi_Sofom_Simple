using Anfx.Profuturo.Sofom.Application.Common.Saga;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class BuscarColoniaStep(IDatabaseService databaseServices) : ISagaStep<CreateSolicitudContext>
{
    private readonly IDatabaseService _databaseServices = databaseServices;

    public async Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        var domicilio = context.Model.Domicilio;

        string colonia = domicilio.Colonia;
        string municipio = domicilio.Delegacion;
        string estado = domicilio.Estado;
        string codigoPostal = domicilio.CP;
        string ciudad = domicilio.Ciudad;
        string pais = domicilio.Pais;
        try
        {
            var IdColonia = await _databaseServices.GetOrCreateColoniaIdAsync(
                colonia,
                municipio,
                estado,
                codigoPostal,
                ciudad,
                pais
            );

            domicilio.IdColonia = IdColonia;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }

    }
}
