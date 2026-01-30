namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class COT_Cotizador_BeneficiosAdicionales
{
    public int Id { get; set; }

    public int? IdCotizador { get; set; }

    public int? IdBeneficioAdicional { get; set; }

    public decimal? Monto { get; set; }

    public BeneficiosAdicionales IdBeneficioAdicionalNavigation { get; set; }

    public COT_Cotizador IdCotizadorNavigation { get; set; }
}