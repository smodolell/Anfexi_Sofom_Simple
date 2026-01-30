using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_ExpedienteArchivoConfiguration : IEntityTypeConfiguration<EXP_ExpedienteArchivo>
{
    public void Configure(EntityTypeBuilder<EXP_ExpedienteArchivo> entity)
    {
        entity.HasKey(e => e.IdExpedienteArchivo).HasFillFactor(90);

        entity.ToTable("EXP_ExpedienteArchivo");

        entity.HasIndex(e => e.IdExpedienteDocumento, "_dta_index_EXP_ExpedienteArchivo_9_1250819518__K2_1_3_4_5_7_9987_4364");

        entity.HasIndex(e => e.NombreUnico, "_dta_index_EXP_ExpedienteArchivo_9_1250819518__K3_1_2_4_5_6_7_9987_4364").HasFillFactor(90);

        entity.Property(e => e.IdExpedienteArchivo).ValueGeneratedNever();
        entity.Property(e => e.ContentType)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.DatoExtra).HasMaxLength(500);
        entity.Property(e => e.Extension)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.FechaSubida).HasColumnType("datetime");
        entity.Property(e => e.NombreReal)
            .IsRequired()
            .HasMaxLength(300)
            .IsUnicode(false);
        entity.Property(e => e.NombreUnico)
            .IsRequired()
            .HasMaxLength(300)
            .IsUnicode(false);

        entity.HasOne(d => d.IdExpedienteDocumentoNavigation).WithMany(p => p.EXP_ExpedienteArchivo)
            .HasForeignKey(d => d.IdExpedienteDocumento)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EXP_ExpedienteArchivo_EXP_ExpedienteDocumento");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<EXP_ExpedienteArchivo> entity);
}
