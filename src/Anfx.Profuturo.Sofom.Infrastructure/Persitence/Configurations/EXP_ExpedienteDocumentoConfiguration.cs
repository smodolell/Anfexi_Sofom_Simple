using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_ExpedienteDocumentoConfiguration : IEntityTypeConfiguration<EXP_ExpedienteDocumento>
{
    public void Configure(EntityTypeBuilder<EXP_ExpedienteDocumento> entity)
    {
        entity.HasKey(e => e.IdExpedienteDocumento).HasFillFactor(90);

        entity.ToTable("EXP_ExpedienteDocumento");

        entity.HasIndex(e => e.IdDocumentoConfig, "dba3_index_71");

        entity.HasIndex(e => e.IdExpediente, "index_EXP_ExpedienteDocumentoIdExpediente").HasFillFactor(90);

        entity.Property(e => e.IdExpedienteDocumento).ValueGeneratedNever();
        entity.Property(e => e.Comentario)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.FechaUltimaModificacion).HasColumnType("datetime");
        entity.Property(e => e.FechaVigencia).HasColumnType("datetime");

        entity.HasOne(d => d.IdDocumentoConfigNavigation).WithMany(p => p.EXP_ExpedienteDocumento)
            .HasForeignKey(d => d.IdDocumentoConfig)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EXP_ExpedienteDocumento_EXP_DocumentoConfig");

        entity.HasOne(d => d.IdExpedienteNavigation).WithMany(p => p.EXP_ExpedienteDocumento)
            .HasForeignKey(d => d.IdExpediente)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EXP_ExpedienteDocumento_EXP_Expediente");

    }

}
