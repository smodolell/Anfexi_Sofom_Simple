using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_SolicitudConfiguration : IEntityTypeConfiguration<OT_Solicitud>
{
    public void Configure(EntityTypeBuilder<OT_Solicitud> entity)
    {
        entity.HasKey(e => e.IdSolicitud).HasFillFactor(90);

        entity.ToTable("OT_Solicitud");

        entity.HasIndex(e => new { e.IdAsesor, e.EsImportada }, "IND_IdSolicitud").HasFillFactor(90);

        entity.HasIndex(e => e.IdContrato, "MejoraDBA02_index_1150380");

        entity.HasIndex(e => e.IdSolicitante, "_dta_index_OT_Solicitud_9_134291538__K3_1_2_4_5_6_7_8_9_10_11_12_14_15_16_17_18_9987_4364");

        entity.HasIndex(e => new { e.IdCotizador, e.IdSolicitante }, "_dta_index_OT_Solicitud_9_134291538__K4_K3_1_5384");

        entity.HasIndex(e => e.IdEstatusSolicitud, "dba_index_13");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.Reestructurado, e.Activo }, "dba_index_132613");

        entity.HasIndex(e => e.FechaAlta, "dba_index_143292");

        entity.HasIndex(e => new { e.EsImportada, e.IdSolicitud, e.IdEstatusSolicitud, e.FechaAlta }, "dba_index_143301");

        entity.HasIndex(e => e.FechaAlta, "dba_index_143303");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.IdAsesor, e.EsImportada }, "dba_index_147614").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusSolicitud, "dba_index_147845");

        entity.HasIndex(e => e.IdEstatusSolicitud, "dba_index_147847");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.FechaAlta }, "dba_index_147863");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.FechaAlta }, "dba_index_147865");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.EsImportada, e.IdSolicitud, e.FechaAlta }, "dba_index_148584");

        entity.HasIndex(e => new { e.Reestructurado, e.FechaAlta, e.Activo }, "dba_index_1487");

        entity.HasIndex(e => new { e.EsImportada, e.FechaAlta }, "dba_index_15");

        entity.HasIndex(e => new { e.Reestructurado, e.FechaAlta, e.Activo }, "dba_index_17");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.Activo }, "dba_index_21");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.Reestructurado }, "dba_index_265");

        entity.HasIndex(e => e.FechaAlta, "dba_index_3315");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.EsImportada }, "dba_index_3409");

        entity.HasIndex(e => new { e.EsImportada, e.Activo }, "dba_index_52");

        entity.HasIndex(e => new { e.EsImportada, e.IdSolicitud, e.FechaAlta }, "dba_index_526");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.Reestructurado, e.FechaAlta, e.Activo }, "dba_index_528");

        entity.HasIndex(e => new { e.IdAsesor, e.Reestructurado, e.Activo }, "dba_index_59");

        entity.HasIndex(e => new { e.IdAsesor, e.EsImportada, e.Activo }, "dba_index_67");

        entity.HasIndex(e => new { e.Reestructurado, e.Activo }, "dba_index_7");

        entity.HasIndex(e => new { e.IdEstatusSolicitud, e.IdAsesor, e.Reestructurado, e.Activo }, "dba_index_91").HasFillFactor(90);

        entity.HasIndex(e => new { e.Reestructurado, e.Activo }, "dba_index_934");

        entity.HasIndex(e => e.EsImportada, "dba_index_95");

        entity.Property(e => e.IdSolicitud).ValueGeneratedNever();
        entity.Property(e => e.Activo).HasDefaultValue(true);
        entity.Property(e => e.EsImportada).HasDefaultValue(false);
        entity.Property(e => e.FechaAlta).HasColumnType("datetime");
        entity.Property(e => e.FechaFirmaContrato).HasColumnType("datetime");
        entity.Property(e => e.FechaPrimeraRenta).HasColumnType("datetime");
        entity.Property(e => e.IdEjecutivo).HasDefaultValue(0);
        entity.Property(e => e.PuntajeScoring).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.PuntajeScoringBuro).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PuntajeScoringCP).HasColumnType("decimal(13, 2)");

        entity.HasOne(d => d.IdAsesorNavigation).WithMany(p => p.OT_Solicitud)
            .HasForeignKey(d => d.IdAsesor)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_Solicitud_Usuario");

        entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.OT_Solicitud)
            .HasForeignKey(d => d.IdContrato)
            .HasConstraintName("FK_OT_Solicitud_Contrato");

        entity.HasOne(d => d.IdCotizadorNavigation).WithMany(p => p.OT_Solicitud)
            .HasForeignKey(d => d.IdCotizador)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_Solicitud_COT_Cotizador1");

        entity.HasOne(d => d.Solicitante).WithMany(p => p.OT_Solicitud)
            .HasForeignKey(d => d.IdSolicitante)
            .HasConstraintName("FK_OT_Solicitud_OT_Solicitante");

        
    }

}
