

namespace Anfx.Profuturo.Sofom.Domain.Entities
{
	public partial class OC_ElementosAuditoria
	{

		public int Id { get; set; }
		public int IdSolicitante { get; set; }

		public string NombreProspecto { get; set; }

		public string CURPProspecto { get; set; }

		public string Folio { get; set; }

		public string? Selfie { get; set; }

		public string? AnversoINE { get; set; }

		public string ReversoINE { get; set; }

		public string CoordenadasUbicacion { get; set; }

		public string TelefonoCelular { get; set; }

		public DateTime FechaHoraConfirmacion { get; set; }

		public DateTime FechaHoraValidacionToken { get; set; }

		public DateTime FechaAlta { get; set; }
		public int PorcSelfieVSINE { get; set; }

		public OT_Solicitante OT_Solicitante { get; set; }
	}
}