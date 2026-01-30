using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class ConsecutivoConfiguration : IEntityTypeConfiguration<Consecutivo>
{
    public void Configure(EntityTypeBuilder<Consecutivo> entity)
    {
        entity.HasKey(e => e.NombreTabla).HasName("PK__Consecut__5BCFB3894AB81AF0");

        entity.ToTable("Consecutivo");

        entity.Property(e => e.NombreTabla)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.FecUltimoCambio).HasColumnType("datetime");

    }

}
