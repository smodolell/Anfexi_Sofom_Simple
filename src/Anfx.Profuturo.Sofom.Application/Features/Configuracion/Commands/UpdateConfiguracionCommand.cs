namespace Anfx.Profuturo.Sofom.Application.Features.Configuracion.Commands;

public class UpdateConfiguracionCommand : ICommand<Result>
{
    public decimal MontoMinimo { get; set; }
    public int PorcentajeActual { get; set; }
    public int TipoPensionId { get; set; }
    public int TipoPrestamoId { get; set; }
    public int TiempoActual { get; set; }
    public decimal PorcSelfieVSINE { get; set; }
}
