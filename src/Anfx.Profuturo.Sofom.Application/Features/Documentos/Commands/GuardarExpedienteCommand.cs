namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Commands;

public record GuardarExpedienteCommand(
    string Folio,
    int IdTipoDocumento,
    string Nombre,
    string MIME,
    string Documento
) : ICommand<Result>;
