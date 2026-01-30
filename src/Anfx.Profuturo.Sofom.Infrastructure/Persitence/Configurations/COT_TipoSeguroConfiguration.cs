using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_TipoSeguroConfiguration : IEntityTypeConfiguration<COT_TipoSeguro>
{
    public void Configure(EntityTypeBuilder<COT_TipoSeguro> entity)
    {
        entity.HasKey(e => e.IdTipoSeguro).HasName("PK__COT_Tipo__472C10CB4CD638E3");

        entity.ToTable("COT_TipoSeguro");

        entity.Property(e => e.Descripcion)
            .HasMaxLength(100)
            .IsUnicode(false);

    }

}
