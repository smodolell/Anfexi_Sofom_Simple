using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_Solicitante_InfoPensionConfiguration : IEntityTypeConfiguration<OT_Solicitante_InfoPension>
{
    public void Configure(EntityTypeBuilder<OT_Solicitante_InfoPension> entity)
    {
        entity.HasKey(e => e.IdInfoPension)
            .HasName("PK__OT_Solic__98F7E0EA45D43599")
            .HasFillFactor(90);

        entity.ToTable("OT_Solicitante_InfoPension");

        entity.HasIndex(e => e.NumeroEmpleado, "Mejora_DBA_1033015").HasFillFactor(90);

        entity.HasIndex(e => e.NumeroOferta, "Mejora_DBA_1610210").HasFillFactor(90);

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_231");

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_33717");

        entity.Property(e => e.FechaJubilacionPension).HasColumnType("datetime");
        entity.Property(e => e.FechaPagoPension).HasColumnType("datetime");
        entity.Property(e => e.FolioIdentificador)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.MontoPension).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.NumeroEmpleado)
            .HasMaxLength(40)
            .IsUnicode(false);
        entity.Property(e => e.NumeroOferta)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.HasOne(d => d.IdSolicitanteNavigation).WithMany(p => p.OT_Solicitante_InfoPension)
            .HasForeignKey(d => d.IdSolicitante)
            .HasConstraintName("OT_Solicitante_InfoPension_OT_Soicitud");

    }

}
