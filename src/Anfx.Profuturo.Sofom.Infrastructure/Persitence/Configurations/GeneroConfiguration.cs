using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class GeneroConfiguration : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> entity)
    {
        entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988173876EA");

        entity.ToTable("Genero");

        entity.Property(e => e.Titulo)
            .HasMaxLength(30)
            .IsUnicode(false);

        
    }

}
