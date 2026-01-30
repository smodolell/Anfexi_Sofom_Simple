using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class ColoniumConfiguration : IEntityTypeConfiguration<Colonia>
{
    public void Configure(EntityTypeBuilder<Colonia> entity)
    {
        entity.HasKey(e => e.IdColonia)
            .HasName("PK__Colonia__A1580F664316F928")
            .HasFillFactor(90);

        entity.HasIndex(e => e.Estado, "_dta_index_Colonia_9_1093578934__K4_5_2657");

        entity.HasIndex(e => new { e.Estado, e.CodigoPostal, e.Municipio }, "_dta_index_Colonia_9_1093578934__K4_K5_K3_1_2_6_1912").HasFillFactor(90);

        entity.HasIndex(e => new { e.Estado, e.CodigoPostal, e.Municipio }, "_dta_index_Colonia_9_1093578934__K4_K5_K3_8066").HasFillFactor(90);

        entity.HasIndex(e => e.CodigoPostal, "_dta_index_Colonia_9_1093578934__K5_8066").HasFillFactor(90);

        entity.Property(e => e.IdColonia).ValueGeneratedNever();
        entity.Property(e => e.Ciudad)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.CodigoPostal)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.Colonia1)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Estado)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Municipio)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Pais)
            .HasMaxLength(200)
            .IsUnicode(false);

    }

}
