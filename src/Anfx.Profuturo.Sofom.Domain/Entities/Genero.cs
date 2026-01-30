

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Genero
{

    public int IdGenero { get; set; }

    public string Titulo { get; set; }

    public ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}