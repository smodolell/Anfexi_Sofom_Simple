

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_SolicitanteDireccion
{

    public int IdDireccion { get; set; }

    public int IdSolicitante { get; set; }

    public int IdTipoDomicilio { get; set; }

    public string Calle { get; set; }

    public string NumInterior { get; set; }

    public int IdColonia { get; set; }

    public int? TiempoEnAnio { get; set; }

    public int? TiempoEnMeses { get; set; }

    public int? IdTipoVivienda { get; set; }

    public decimal? RentaMensual { get; set; }

    public string NumExterior { get; set; }

    public string Pais { get; set; }

    public string TipoDomicilio { get; set; }

    public string EntreCalleUno { get; set; }

    public string EntreCalleDos { get; set; }

    public Colonia IdColoniaNavigation { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }
}