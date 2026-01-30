

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class URL_Response
{

    public int IdUrlResponse { get; set; }

    public int? IdUrlRequest { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string LogSalida { get; set; }

    public string Error { get; set; }
}