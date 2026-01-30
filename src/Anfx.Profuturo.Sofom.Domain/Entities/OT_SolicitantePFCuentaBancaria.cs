

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_SolicitantePFCuentaBancaria
{

    public int IdCuentaBancaria { get; set; }

    public int IdSolicitante { get; set; }

    public string Banco { get; set; }

    public string NumeroTarjeta { get; set; }

    public string TipoCuenta { get; set; }

    public decimal SaldoPromedio { get; set; }

    public DateTime FechaApertura { get; set; }

    public string CuentaBancaria { get; set; }

    public string CuentaClabe { get; set; }

    public int? IdBanco { get; set; }

    public int? IdTipoCuenta { get; set; }

    public int? NroSucursal { get; set; }

    public Banco IdBancoNavigation { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }
}