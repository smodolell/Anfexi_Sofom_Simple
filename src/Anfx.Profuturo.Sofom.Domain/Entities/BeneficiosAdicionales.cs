namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class BeneficiosAdicionales
{
    public int IdBeneficiosAdicionales { get; set; }
    public string Nombre { get; set; }

    public int? Dias { get; set; }
    public decimal? Costo { get; set; }
    public int? TipoSeguro { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? IdFrecuenciaBA { get; set; }
    public bool? Factor { get; set; }
    public decimal? FactorPorcentaje { get; set; }
    public ICollection<COT_Cotizador_BeneficiosAdicionales> COT_Cotizador_BeneficiosAdicionales { get; set; } = new List<COT_Cotizador_BeneficiosAdicionales>();
}