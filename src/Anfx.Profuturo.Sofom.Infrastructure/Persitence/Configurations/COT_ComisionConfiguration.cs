using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_ComisionConfiguration : IEntityTypeConfiguration<COT_Comision>
{
    public void Configure(EntityTypeBuilder<COT_Comision> entity)
    {
        entity.HasKey(e => e.IdComision).HasName("PK_Comision");

        entity.ToTable("COT_Comision");

        entity.Property(e => e.IdComision).ValueGeneratedNever();
        entity.Property(e => e.Comision)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Porcentaje).HasColumnType("decimal(9, 4)");

    
    }

}
