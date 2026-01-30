

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class Empresa
{

    public int IdEmpresa { get; set; }

    public string Empresa1 { get; set; }

    public string RFC { get; set; }

    public string RazonSocial { get; set; }

    public string DireccionEmpresa { get; set; }

    public string Telefono { get; set; }

    public string Representante { get; set; }

    public string AvisosEstadodeCuenta { get; set; }

    public string AdvertenciasEstadodeCuenta { get; set; }

    public string AclaracionesEstadodeCuenta { get; set; }

    public bool? UsaDesembolso { get; set; }

    public bool? Pasivo { get; set; }

    public int? IdDireccionEmpresa { get; set; }

    public ICollection<TipoCredito> TipoCredito { get; set; } = new List<TipoCredito>();
}