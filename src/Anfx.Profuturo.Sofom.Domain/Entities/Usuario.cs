

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Usuario
{

    public int IdUsuario { get; set; }

    public string UserName { get; set; }

    public string UserPass { get; set; }

    public DateTime? FechaRegistracion { get; set; }

    public int? IdUsuarioQRegistro { get; set; }

    public int? IdRol { get; set; }

    public string NombreCompleto { get; set; }

    public string Email { get; set; }

    public bool? Activo { get; set; }

    public int IdGenero { get; set; }

    public bool? EsPrimerInicio { get; set; }

    public bool? RequiereCambioPass { get; set; }

    public string IdiomaUI { get; set; }

    public int IdTipoAsesor { get; set; }

    public string Celular { get; set; }

    public string NumeroEmpleado { get; set; }

    public int? IdUnidadNegocio { get; set; }

    public bool? AutorizacionCIF { get; set; }

    public bool? EsGerente { get; set; }

    public bool? AutorizacionCB { get; set; }

    public string EmployeeID { get; set; }

    public Genero IdGeneroNavigation { get; set; }

    public ICollection<OT_FaseHistoria> OT_FaseHistoria { get; set; } = new List<OT_FaseHistoria>();

    public ICollection<OT_Solicitud> OT_Solicitud { get; set; } = new List<OT_Solicitud>();
}