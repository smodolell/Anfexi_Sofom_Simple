using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class CompaniaTelefonicaConfiguration : IEntityTypeConfiguration<CompaniaTelefonica>
{
    public void Configure(EntityTypeBuilder<CompaniaTelefonica> entity)
    {
        entity.HasKey(e => e.IdCiaTelefonica).HasName("PK__Compania__617BCEC2328C56FB");

        entity.ToTable("CompaniaTelefonica");

        entity.Property(e => e.Descripcion)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<CompaniaTelefonica> entity);
}
