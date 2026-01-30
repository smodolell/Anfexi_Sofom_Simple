namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class COT_CalendarioFechasCorte
{
    public int IdCalendarioFechaCorte { get; set; }

    public DateTime? DiaInicio { get; set; }

    public int IdAgencia { get; set; }

    public int IdPeriodicidad { get; set; }

    public COT_Agencia IdAgenciaNavigation { get; set; }

    public SB_Periodicidad IdPeriodicidadNavigation { get; set; }
}