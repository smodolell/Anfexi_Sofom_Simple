namespace Anfx.Profuturo.Sofom.Domain.Entities;

public class Configuracion
{
	public int Id { get; set; }
	public decimal MontoMinimo { get; set; }
	public int PorcentajeActual { get; set; }
	public int TipoPensionId { get; set; }
	public int TipoPrestamoId { get; set; }
	public int TiempoActual { get; set; }
	public decimal PorcSelfieVSINE { get; set; }

	public TipoPension TipoPension { get; set; }
	public TipoPrestamo TipoPrestamo { get; set; }
}
