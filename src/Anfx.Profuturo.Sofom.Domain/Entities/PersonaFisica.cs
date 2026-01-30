

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class PersonaFisica
{

    public int IdPersona { get; set; }

    public int? IdEstadoCivil { get; set; }

    public string Sexo { get; set; }

    public string CURP { get; set; }

    public Persona IdPersonaNavigation { get; set; }
}