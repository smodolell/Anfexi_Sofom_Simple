using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_DocumentoConfiguration : IEntityTypeConfiguration<EXP_Documento>
{
    public void Configure(EntityTypeBuilder<EXP_Documento> entity)
    {
        entity.HasKey(e => e.IdDocumento);

        entity.ToTable("EXP_Documento");

        entity.Property(e => e.IdDocumento).ValueGeneratedNever();
        entity.Property(e => e.Descripcion)
            .HasMaxLength(150)
            .IsUnicode(false);
        entity.Property(e => e.NombreArchivo)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.NombreDocumento)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Orden).HasColumnType("decimal(12, 2)");

        entity.HasOne(d => d.IdTipoDocumentacionNavigation).WithMany(p => p.EXP_Documento)
            .HasForeignKey(d => d.IdTipoDocumentacion)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EXP_Documento_EXP_TipoDocumentacion");

    }

}
