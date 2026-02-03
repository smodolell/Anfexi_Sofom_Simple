using System.ComponentModel.DataAnnotations.Schema;

namespace Anfx.Profuturo.Sofom.Domain.Procedures;


public partial class usp_SaldoLiquidacionResult
{
    [Column("Saldo", TypeName = "decimal(15,2)")]
    public decimal? Saldo { get; set; }
}