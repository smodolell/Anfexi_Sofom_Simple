using Anfx.Profuturo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Anfx.Profuturo.Sofom.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }

    DbSet<Banco> Banco { get; set; }

    DbSet<BeneficiosAdicionales> BeneficiosAdicionales { get; set; }
    DbSet<BitacoraCodigos> BitacoraCodigos { get; set; }
    DbSet<CatTipoDocumentoExpediente> CatTipoDocumentoExpedientes { get; set; }
    DbSet<CatTiposMimeDocumento> CatTiposMimeDocumentos { get; set; }
    DbSet<COT_Agencia> COT_Agencia { get; set; }

    DbSet<COT_Calculadora> COT_Calculadora { get; set; }

    DbSet<COT_CalendarioFechasCorte> COT_CalendarioFechasCorte { get; set; }

    DbSet<COT_Comision> COT_Comision { get; set; }

    DbSet<COT_Cotizador> COT_Cotizador { get; set; }

    DbSet<COT_Cotizador_BeneficiosAdicionales> COT_Cotizador_BeneficiosAdicionales { get; set; }

    DbSet<COT_Plan> COT_Plan { get; set; }

    DbSet<COT_PlanComision> COT_PlanComision { get; set; }

    DbSet<COT_PlanPlazo> COT_PlanPlazo { get; set; }

    DbSet<COT_PlanTasa> COT_PlanTasa { get; set; }

    DbSet<COT_Plazo> COT_Plazo { get; set; }

    DbSet<COT_SolicitudCotizador> COT_SolicitudCotizador { get; set; }

    DbSet<COT_TablaAmortiza> COT_TablaAmortiza { get; set; }

    DbSet<COT_Tasa> COT_Tasa { get; set; }

    DbSet<COT_TipoSeguro> COT_TipoSeguro { get; set; }

    DbSet<Colonia> Colonia { get; set; }

    DbSet<CompaniaTelefonica> CompaniaTelefonica { get; set; }

    DbSet<Consecutivo> Consecutivo { get; set; }

    DbSet<Contrato> Contrato { get; set; }

    DbSet<EXP_Documento> EXP_Documento { get; set; }

    DbSet<EXP_DocumentoConfig> EXP_DocumentoConfig { get; set; }

    DbSet<EXP_Expediente> EXP_Expediente { get; set; }

    DbSet<EXP_ExpedienteArchivo> EXP_ExpedienteArchivo { get; set; }

    DbSet<EXP_ExpedienteDocumento> EXP_ExpedienteDocumento { get; set; }

    DbSet<EXP_TipoDocumentacion> EXP_TipoDocumentacion { get; set; }

    DbSet<Empresa> Empresa { get; set; }

    DbSet<EstatusContrato> EstatusContrato { get; set; }

    DbSet<Genero> Genero { get; set; }

    DbSet<Movimiento> Movimiento { get; set; }

    DbSet<OT_EstatusFase> OT_EstatusFase { get; set; }

    DbSet<OT_Fase> OT_Fase { get; set; }

    DbSet<OT_FaseHistoria> OT_FaseHistoria { get; set; }

    DbSet<OT_Solicitante> OT_Solicitante { get; set; }

    DbSet<OT_SolicitanteDireccion> OT_SolicitanteDireccion { get; set; }

    DbSet<OT_SolicitantePF> OT_SolicitantePF { get; set; }

    DbSet<OT_SolicitantePFCuentaBancaria> OT_SolicitantePFCuentaBancaria { get; set; }

    DbSet<OT_Solicitante_InfoPension> OT_Solicitante_InfoPension { get; set; }

    DbSet<OT_Solicitud> OT_Solicitud { get; set; }

    DbSet<OT_TipoDocImprimir> OT_TipoDocImprimir { get; set; }

    DbSet<Pago> Pago { get; set; }

    DbSet<Persona> Persona { get; set; }

    DbSet<PersonaDireccion> PersonaDireccion { get; set; }

    DbSet<PersonaFisica> PersonaFisica { get; set; }

    DbSet<RC_Proceso> RC_Proceso { get; set; }

    DbSet<RC_ProcesoEstado> RC_ProcesoEstado { get; set; }

    DbSet<RC_ProcesoHistorial> RC_ProcesoHistorial { get; set; }

    DbSet<RC_Reestructuracion> RC_Reestructuracion { get; set; }

    DbSet<RangoFecha> RangoFecha { get; set; }

    DbSet<RelPagoMovimiento> RelPagoMovimiento { get; set; }

    DbSet<SB_Periodicidad> SB_Periodicidad { get; set; }

    DbSet<TablaAmortiza> TablaAmortiza { get; set; }

    DbSet<TipoCredito> TipoCredito { get; set; }

    DbSet<URL_DatosSolicitud> URL_DatosSolicitud { get; set; }

    DbSet<URL_RegistroEnvios> URL_RegistroEnvios { get; set; }

    DbSet<URL_Request> URL_Request { get; set; }

    DbSet<URL_Response> URL_Response { get; set; }

    DbSet<URL_Solicitud> URL_Solicitud { get; set; }

    DbSet<Usuario> Usuario { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
