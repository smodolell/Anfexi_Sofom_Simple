namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class Colonia
{
    public int IdColonia { get; set; }
    public string Colonia1 { get; set; }

    public string Municipio { get; set; }
    public string Estado { get; set; }
    public string CodigoPostal { get; set; }
    public string Ciudad { get; set; }
    public string Pais { get; set; }
    public ICollection<COT_Agencia> COT_Agencia { get; set; } = new List<COT_Agencia>();
    public ICollection<OT_SolicitanteDireccion> OT_SolicitanteDireccion { get; set; } = new List<OT_SolicitanteDireccion>();
    public ICollection<PersonaDireccion> PersonaDireccion { get; set; } = new List<PersonaDireccion>();
}