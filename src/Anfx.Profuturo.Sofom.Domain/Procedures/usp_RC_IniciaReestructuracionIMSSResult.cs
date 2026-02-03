using System.ComponentModel.DataAnnotations;

namespace Anfx.Profuturo.Domain.Entities;

public partial class usp_RC_IniciaReestructuracionIMSSResult
{
    public int? IdReestructuraActual { get; set; }
    [StringLength(100)]
    public string Mensaje { get; set; } = string.Empty;
    public bool? EsError { get; set; }
}
