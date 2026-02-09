namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class COT_Agencia
{
    public int IdAgencia { get; set; }

    public string? Nombre { get; set; }

    public string Representante { get; set; } = string.Empty;

    public string? NombrePersona { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdPersonaJuridica { get; set; }

    public string? RFC { get; set; }

    public string? Identificador { get; set; }

    public DateTime? FechaAlta { get; set; }

    public string? Email { get; set; }

    public int? IdUnidadNegocio { get; set; }

    public string? Calle { get; set; }

    public string? NumExterior { get; set; }

    public string? NumInterior { get; set; }

    public int? IdColonia { get; set; }

    public int? IdTipoDireccion { get; set; }

    public int? IdTipoTelefono { get; set; }

    public string? Telefono { get; set; }

    public string? NombreContacto { get; set; }

    public string? Extension { get; set; }

    public string? IdClientePeopleSoft { get; set; }

    public string? ReferenciaCIF { get; set; }

    public ICollection<Banco> Banco { get; set; } = new List<Banco>();

    public ICollection<COT_CalendarioFechasCorte> COT_CalendarioFechasCorte { get; set; } = new List<COT_CalendarioFechasCorte>();

    public ICollection<COT_Cotizador> COT_Cotizador { get; set; } = new List<COT_Cotizador>();
    public Colonia IdColoniaNavigation { get; set; }
    public ICollection<RC_Reestructuracion> RC_Reestructuracion { get; set; } = new List<RC_Reestructuracion>();
}