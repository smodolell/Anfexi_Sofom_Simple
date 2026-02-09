using Anfx.Profuturo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Infrastructure.Data.Configurations;

public class CatTipoDocumentoExpedienteConfiguration : IEntityTypeConfiguration<CatTipoDocumentoExpediente>
{
    public void Configure(EntityTypeBuilder<CatTipoDocumentoExpediente> entity)
    {
        entity
            .HasNoKey()
            .ToTable("CatTipoDocumentoExpediente");

        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.TipoDocumento)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasDefaultValue("");

    }

}
