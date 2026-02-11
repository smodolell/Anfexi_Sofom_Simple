namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class Banco
{
    public int IdBanco { get; set; }
    public string? Banco1 { get; set; }
    public string? Clave_SIES { get; set; }
    public int IdAgencia { get; set; }
    public int? IdBancoNIK { get; set; }
    public int? IdCatalogoServicioCert { get; set; }
    //public bool ActiveAC { get; set; }
    public COT_Agencia Agencia { get; set; } = null!;
    public ICollection<OT_SolicitantePFCuentaBancaria> OT_SolicitantePFCuentaBancaria { get; set; } = new List<OT_SolicitantePFCuentaBancaria>();
}