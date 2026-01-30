

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_EstatusFase
{

    public int IdEstatusFase { get; set; }

    public string Titulo { get; set; }

    public string Icono { get; set; }

    public bool PorDefecto { get; set; }

    public ICollection<OT_FaseHistoria> OT_FaseHistoria { get; set; } = new List<OT_FaseHistoria>();
}