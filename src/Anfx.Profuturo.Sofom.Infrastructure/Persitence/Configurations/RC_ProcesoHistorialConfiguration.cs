using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RC_ProcesoHistorialConfiguration : IEntityTypeConfiguration<RC_ProcesoHistorial>
{
    public void Configure(EntityTypeBuilder<RC_ProcesoHistorial> entity)
    {
        entity.HasKey(e => e.IdProcesoHistorial)
            .HasName("PK__RC_Proce__B6553BE047677850")
            .HasFillFactor(90);

        entity.ToTable("RC_ProcesoHistorial");

        entity.HasIndex(e => new { e.IdReestructuracion, e.IdProceso }, "Mejora_IDX_17");

        entity.HasIndex(e => new { e.IdReestructuracion, e.IdEstadoProceso }, "Mejora_IDX_269").HasFillFactor(90);

        entity.HasIndex(e => e.IdReestructuracion, "Mejora_IDX_308");

        entity.HasIndex(e => new { e.IdReestructuracion, e.IdProceso, e.IdEstadoProceso, e.EsProcesoActual }, "Mejora_IDX_35239").HasFillFactor(90);

        entity.Property(e => e.Comentarios)
            .HasMaxLength(600)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.FechaRegistro)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.FechaUltimaModificacion).HasColumnType("datetime");

        entity.HasOne(d => d.IdEstadoProcesoNavigation).WithMany(p => p.RC_ProcesoHistorial)
            .HasForeignKey(d => d.IdEstadoProceso)
            .HasConstraintName("FK__RC_Proces__IdEst__4B380934");

        entity.HasOne(d => d.IdReestructuracionNavigation).WithMany(p => p.RC_ProcesoHistorial)
            .HasForeignKey(d => d.IdReestructuracion)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RC_Proces__IdRee__494FC0C2");

    }

}
