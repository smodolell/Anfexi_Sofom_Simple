using Anfx.Profuturo.Sofom.Application.Features.Configuracion.DTOs;

namespace Anfx.Profuturo.Sofom.Application.Features.Configuracion.Queries;

public record GetConfiguracionQuery : IQuery<Result<ConfiguracionDto>>;
