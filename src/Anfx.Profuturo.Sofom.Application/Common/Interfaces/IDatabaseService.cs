using Anfx.Profuturo.Domain.Entities;
using Anfx.Profuturo.Sofom.Application.Common.Dtos;

namespace Anfx.Profuturo.Sofom.Application.Common.Interfaces;

public interface IDatabaseService
{
    Task<int> usp_ProcesaSaldoInsolutoAsync(int? idContrato, CancellationToken cancellationToken = default);
    Task<List<usp_SaldoLiquidacionResult>> usp_SaldoLiquidacionAsync(int? idContrato, DateTime? fecha, CancellationToken cancellationToken = default);
    Task<List<usp_RC_IniciaReestructuracionIMSSResult>> usp_RC_IniciaReestructuracionIMSSAsync(int? idContrato, CancellationToken cancellationToken = default);

    int? GetIdDelegacionIMMS(string dependenciaIMSS);
    int? GetIdCiaTelefonica(string ciaTelefonica);

    Task CreateOneClickUserAsync();


    Task<int> GetOrCreateColoniaIdAsync(
             string? colonia,
             string? municipio,
             string? estado,
             string? codigoPostal,
             string? ciudad,
             string? pais);
    List<DocumentoConfigItem> ObtenerDocumentosConfigurados(
        int expedienteId,
        int idUsuario,
        int idAgencia);


}

