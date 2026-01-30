using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class SB_PeriodicidadConfiguration : IEntityTypeConfiguration<SB_Periodicidad>
{
    public void Configure(EntityTypeBuilder<SB_Periodicidad> entity)
    {
        entity.HasKey(e => e.IdPeriodicidad)
            .HasName("PK__SB_Perio__DA476CCD02084FDA")
            .HasFillFactor(90);

        entity.ToTable("SB_Periodicidad");

        entity.Property(e => e.CveCortaPeriodicidad).HasMaxLength(4);
        entity.Property(e => e.DescPeriodicidad).HasMaxLength(25);
        entity.Property(e => e.NroPagosAnio).HasDefaultValue(365);

    }

}
