

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class PersonaDireccion
{

    public int IdPersonaDireccion { get; set; }

    public int IdPersona { get; set; }

    public string Calle { get; set; }

    public string NumExterior { get; set; }

    public string NumInterior { get; set; }

    public int IdColonia { get; set; }

    public int? IdTipoDireccion { get; set; }

    public string Pais { get; set; }

    public Colonia IdColoniaNavigation { get; set; }

    public Persona IdPersonaNavigation { get; set; }
}