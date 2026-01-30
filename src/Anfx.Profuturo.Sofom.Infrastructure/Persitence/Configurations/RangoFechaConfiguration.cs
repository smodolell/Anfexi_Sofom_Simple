using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RangoFechaConfiguration : IEntityTypeConfiguration<RangoFecha>
{
    public void Configure(EntityTypeBuilder<RangoFecha> entity)
    {
        entity.HasKey(e => e.IdRangoEdad).HasFillFactor(90);

        entity.ToTable("RangoFecha");

        entity.Property(e => e.Descricpion)
            .HasMaxLength(250)
            .IsUnicode(false);
        entity.Property(e => e.MontoPrestamo).HasColumnType("decimal(18, 2)");

        entity.HasOne(d => d.IdCOT_PlanNavigation).WithMany(p => p.RangoFecha)
            .HasForeignKey(d => d.IdCOT_Plan)
            .HasConstraintName("FK_RangoFecha_COT_Plan");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<RangoFecha> entity);
}
