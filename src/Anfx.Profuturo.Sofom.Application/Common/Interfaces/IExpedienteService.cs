namespace Anfx.Profuturo.Sofom.Application.Common.Interfaces;

public interface IExpedienteService
{
    Task<int> CreateOrUpdateExpediente(int idDuenioExpediente, int idQuePersona, int idUsuario, int idAgencia);
}
