using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RC_ProcesoConfiguration : IEntityTypeConfiguration<RC_Proceso>
{
    public void Configure(EntityTypeBuilder<RC_Proceso> entity)
    {
        entity.HasKey(e => e.IdProceso).HasName("PK__RC_Proce__036D07433A0D7D32");

        entity.ToTable("RC_Proceso");

        entity.HasIndex(e => e.ClaveProceso, "UQ__RC_Proce__8E49DA663CE9E9DD").IsUnique();

        entity.Property(e => e.IdProceso).ValueGeneratedNever();
        entity.Property(e => e.Accion)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.ClaveProceso)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.Controlador)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NombreProceso)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.spAprobacion)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.spCancelacion)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasDefaultValue("");

    }

}
