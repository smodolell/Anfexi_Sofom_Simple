using Anfx.Profuturo.Domain.Entities;
using Anfx.Profuturo.Sofom.Application.Common.Interfaces;
using Anfx.Profuturo.Sofom.Infrastructure.Persitence.Interfases;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence;

public partial class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public virtual DbSet<Banco> Banco { get; set; }

    public virtual DbSet<BeneficiosAdicionales> BeneficiosAdicionales { get; set; }

    public virtual DbSet<BitacoraCodigos> BitacoraCodigos { get; set; }
    public virtual DbSet<CatTipoDocumentoExpediente> CatTipoDocumentoExpedientes { get; set; }
    public virtual DbSet<CatTiposMimeDocumento> CatTiposMimeDocumentos { get; set; }

    public virtual DbSet<CompaniaTelefonica> CompaniaTelefonicas { get; set; }
    public virtual DbSet<COT_Agencia> COT_Agencia { get; set; }

    public virtual DbSet<COT_Calculadora> COT_Calculadora { get; set; }

    public virtual DbSet<COT_CalendarioFechasCorte> COT_CalendarioFechasCorte { get; set; }

    public virtual DbSet<COT_Comision> COT_Comision { get; set; }

    public virtual DbSet<COT_Cotizador> COT_Cotizador { get; set; }

    public virtual DbSet<COT_Cotizador_BeneficiosAdicionales> COT_Cotizador_BeneficiosAdicionales { get; set; }

    public virtual DbSet<COT_Plan> COT_Plan { get; set; }

    public virtual DbSet<COT_PlanComision> COT_PlanComision { get; set; }

    public virtual DbSet<COT_PlanPlazo> COT_PlanPlazo { get; set; }

    public virtual DbSet<COT_PlanTasa> COT_PlanTasa { get; set; }

    public virtual DbSet<COT_Plazo> COT_Plazo { get; set; }

    public virtual DbSet<COT_SolicitudCotizador> COT_SolicitudCotizador { get; set; }

    public virtual DbSet<COT_TablaAmortiza> COT_TablaAmortiza { get; set; }

    public virtual DbSet<COT_Tasa> COT_Tasa { get; set; }

    public virtual DbSet<COT_TipoSeguro> COT_TipoSeguro { get; set; }

    public virtual DbSet<Colonia> Colonia { get; set; }

    public virtual DbSet<Consecutivo> Consecutivo { get; set; }

    public virtual DbSet<Contrato> Contrato { get; set; }

    public virtual DbSet<EXP_Documento> EXP_Documento { get; set; }

    public virtual DbSet<EXP_DocumentoConfig> EXP_DocumentoConfig { get; set; }

    public virtual DbSet<EXP_Expediente> EXP_Expediente { get; set; }

    public virtual DbSet<EXP_ExpedienteArchivo> EXP_ExpedienteArchivo { get; set; }

    public virtual DbSet<EXP_ExpedienteDocumento> EXP_ExpedienteDocumento { get; set; }

    public virtual DbSet<EXP_TipoDocumentacion> EXP_TipoDocumentacion { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<EstatusContrato> EstatusContrato { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Movimiento> Movimiento { get; set; }

    public virtual DbSet<OT_EstatusFase> OT_EstatusFase { get; set; }

    public virtual DbSet<OT_Fase> OT_Fase { get; set; }

    public virtual DbSet<OT_FaseHistoria> OT_FaseHistoria { get; set; }

    public virtual DbSet<OT_Solicitante> OT_Solicitante { get; set; }

    public virtual DbSet<OT_SolicitanteDireccion> OT_SolicitanteDireccion { get; set; }

    public virtual DbSet<OT_SolicitantePF> OT_SolicitantePF { get; set; }

    public virtual DbSet<OT_SolicitantePFCuentaBancaria> OT_SolicitantePFCuentaBancaria { get; set; }

    public virtual DbSet<OT_Solicitante_InfoPension> OT_Solicitante_InfoPension { get; set; }

    public virtual DbSet<OT_Solicitud> OT_Solicitud { get; set; }

    public virtual DbSet<OT_TipoDocImprimir> OT_TipoDocImprimir { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Persona> Persona { get; set; }

    public virtual DbSet<PersonaDireccion> PersonaDireccion { get; set; }

    public virtual DbSet<PersonaFisica> PersonaFisica { get; set; }

    public virtual DbSet<RC_Proceso> RC_Proceso { get; set; }

    public virtual DbSet<RC_ProcesoEstado> RC_ProcesoEstado { get; set; }

    public virtual DbSet<RC_ProcesoHistorial> RC_ProcesoHistorial { get; set; }

    public virtual DbSet<RC_Reestructuracion> RC_Reestructuracion { get; set; }

    public virtual DbSet<RangoFecha> RangoFecha { get; set; }

    public virtual DbSet<RelPagoMovimiento> RelPagoMovimiento { get; set; }

    public virtual DbSet<SB_Periodicidad> SB_Periodicidad { get; set; }

    public virtual DbSet<TablaAmortiza> TablaAmortiza { get; set; }

    public virtual DbSet<TipoCredito> TipoCredito { get; set; }

    public virtual DbSet<URL_DatosSolicitud> URL_DatosSolicitud { get; set; }

    public virtual DbSet<URL_RegistroEnvios> URL_RegistroEnvios { get; set; }

    public virtual DbSet<URL_Request> URL_Request { get; set; }

    public virtual DbSet<URL_Response> URL_Response { get; set; }

    public virtual DbSet<URL_Solicitud> URL_Solicitud { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Configuracion> Configuraciones { get; set; }
    public virtual DbSet<TipoPension> TiposPension { get; set; }
    public virtual DbSet<TipoPrestamo> TiposPrestamo { get; set; }
    public virtual DbSet<OC_ElementosAuditoria> OC_ElementosAuditoria { get; set; }
    public virtual DbSet<TipoPension> TipoPensiones { get; set; }
    public virtual DbSet<TipoPrestamo> TipoPrestamos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }


}


public partial class ApplicationDbContext
{
    private IApplicationDbContextProcedures _procedures;

    public virtual IApplicationDbContextProcedures Procedures
    {
        get
        {
            if (_procedures is null) _procedures = new ApplicationDbContextProcedures(this);
            return _procedures;
        }
        set
        {
            _procedures = value;
        }
    }

    public IApplicationDbContextProcedures GetProcedures()
    {
        return Procedures;
    }
}
