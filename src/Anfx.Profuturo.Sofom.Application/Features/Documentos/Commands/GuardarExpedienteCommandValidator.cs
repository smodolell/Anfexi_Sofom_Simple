using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.Commands;

public class GuardarExpedienteCommandValidator : AbstractValidator<GuardarExpedienteCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public GuardarExpedienteCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Folio)
           .NotEmpty().WithMessage("El folio es un campo requerido y no puede estar vacío")
           .MustAsync(async (folio, cancellation) =>
            {
                if (string.IsNullOrEmpty(folio))
                    return false;
                return await _dbContext.COT_Cotizador.AnyAsync(r => r.Folio.Equals(folio));
            }).WithMessage("El folio no existe");

        RuleFor(x => x.IdTipoDocumento)
            .GreaterThan(0)
            .WithMessage("El IdTipoDocumento debe ser mayor a 0")
            .WithErrorCode("1002")
            .MustAsync(ValidarTipoDocumentoAsync)
            .WithMessage("El tipo de documento no es válido");


        RuleFor(x => x.MIME)
            .NotEmpty()
            .WithMessage("El MIME es requerido")
            .MustAsync(ValidarTipoMimeAsync)
            .WithMessage("El tipo MIME no es válido");

        RuleFor(x => x.Documento)
          .NotEmpty()
          .WithMessage("El documento es requerido")
          .Must(BeValidBase64)
          .WithMessage("El documento debe ser una cadena Base64 válida")
          .When(x => !string.IsNullOrEmpty(x.Documento));
    }

    private async Task<bool> ValidarTipoDocumentoAsync(int idTipoDocumento, CancellationToken cancellationToken)
    {
        var tipoDocumento = await _dbContext.CatTipoDocumentoExpedientes
            .FirstOrDefaultAsync(r => r.Id == idTipoDocumento);
        return tipoDocumento != null;
    }

    private async Task<bool> ValidarTipoMimeAsync(string mime, CancellationToken cancellationToken)
    {
        var tipoMime = await _dbContext.CatTiposMimeDocumentos.FirstOrDefaultAsync(r => r.TipoMIME.Equals(mime));
        return tipoMime != null;
    }

    private bool BeValidBase64(string base64)
    {
        if (string.IsNullOrEmpty(base64)) return true;

        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
