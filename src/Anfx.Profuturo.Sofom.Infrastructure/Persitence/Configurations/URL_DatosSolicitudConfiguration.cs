using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class URL_DatosSolicitudConfiguration : IEntityTypeConfiguration<URL_DatosSolicitud>
{
    public void Configure(EntityTypeBuilder<URL_DatosSolicitud> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("PK__URL_Dato__3214EC076C6190AB")
            .HasFillFactor(90);

        entity.ToTable("URL_DatosSolicitud");

        entity.Property(e => e.ConceptoInforme)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.DummyField1)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.DummyField2)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.DummyField3)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.DummyField4)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.DummyField5)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.FechaInforme)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Folio)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.FolioConfirmacion)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.Ubicacion)
            .HasMaxLength(100)
            .IsUnicode(false);

    }

}
