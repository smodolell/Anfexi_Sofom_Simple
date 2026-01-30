using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class BeneficiosAdicionaleConfiguration : IEntityTypeConfiguration<BeneficiosAdicionales>
{
    public void Configure(EntityTypeBuilder<BeneficiosAdicionales> entity)
    {
        entity.HasKey(e => e.IdBeneficiosAdicionales).HasName("PK__Benefici__9445C06A546A86BC");

        entity.Property(e => e.Costo).HasColumnType("decimal(12, 3)");
        entity.Property(e => e.Factor).HasDefaultValue(false);
        entity.Property(e => e.FactorPorcentaje).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.FechaFin).HasColumnType("datetime");
        entity.Property(e => e.FechaInicio).HasColumnType("datetime");
        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsUnicode(false);

    }

}
