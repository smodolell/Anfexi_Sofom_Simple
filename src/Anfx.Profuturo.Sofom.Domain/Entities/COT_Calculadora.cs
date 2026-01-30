namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class COT_Calculadora
{
    public int IdCalculadora { get; set; }

    public bool? CapacidadDescuento { get; set; }

    public bool? MontoPensionSueldo { get; set; }

    public bool? MontoPrestamo { get; set; }

    public bool? MontoDepositar { get; set; }

    public int? IdPlan { get; set; }
}