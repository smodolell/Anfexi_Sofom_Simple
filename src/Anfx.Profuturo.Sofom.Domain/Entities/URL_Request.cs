

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class URL_Request
{

    public int IdUrlRequest { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string Metodo { get; set; }

    public string LogEntrada { get; set; }

    public string token { get; set; }
}