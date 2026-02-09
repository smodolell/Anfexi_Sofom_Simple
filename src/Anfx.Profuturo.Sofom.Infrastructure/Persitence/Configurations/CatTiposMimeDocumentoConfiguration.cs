using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class CatTiposMimeDocumentoConfiguration : IEntityTypeConfiguration<CatTiposMimeDocumento>
    {
        public void Configure(EntityTypeBuilder<CatTiposMimeDocumento> entity)
        {
            entity
                .HasNoKey()
                .ToTable("CatTiposMimeDocumento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Extencion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TipoMIME)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CatTiposMimeDocumento> entity);
    }
}
