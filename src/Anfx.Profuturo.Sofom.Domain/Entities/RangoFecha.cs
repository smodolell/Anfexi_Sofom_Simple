namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class RangoFecha
{

    public int IdRangoEdad { get; set; }

    public int? EdadMinima { get; set; }

    public int? EdadMaxima { get; set; }

    public decimal? MontoPrestamo { get; set; }

    public string? Descricpion { get; set; }

    public int? IdCOT_Plan { get; set; }

    public COT_Plan IdCOT_PlanNavigation { get; set; }
}