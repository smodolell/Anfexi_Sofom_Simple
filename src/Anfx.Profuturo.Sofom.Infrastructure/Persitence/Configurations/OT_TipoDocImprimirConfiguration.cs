using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_TipoDocImprimirConfiguration : IEntityTypeConfiguration<OT_TipoDocImprimir>
{
    public void Configure(EntityTypeBuilder<OT_TipoDocImprimir> entity)
    {
        entity.HasKey(e => e.IdTipoDocImprimir).HasFillFactor(90);

        entity.ToTable("OT_TipoDocImprimir");

        entity.Property(e => e.IdTipoDocImprimir).ValueGeneratedNever();
        entity.Property(e => e.NombreArchivo)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NombreIdentificacionArchivo)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.Orden).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

    }

}
