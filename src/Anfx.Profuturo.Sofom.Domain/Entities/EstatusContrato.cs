

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class EstatusContrato
{

    public int IdEstatusContrato { get; set; }

    public string EstatusContrato1 { get; set; }

    public ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();
}