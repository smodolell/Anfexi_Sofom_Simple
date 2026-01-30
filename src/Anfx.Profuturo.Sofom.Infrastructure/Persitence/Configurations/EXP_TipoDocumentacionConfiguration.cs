using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_TipoDocumentacionConfiguration : IEntityTypeConfiguration<EXP_TipoDocumentacion>
{
    public void Configure(EntityTypeBuilder<EXP_TipoDocumentacion> entity)
    {
        entity.HasKey(e => e.IdTipoDocumentacion);

        entity.ToTable("EXP_TipoDocumentacion");

        entity.Property(e => e.IdTipoDocumentacion).ValueGeneratedNever();
        entity.Property(e => e.Orden).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(150)
            .IsUnicode(false);


    }

}
