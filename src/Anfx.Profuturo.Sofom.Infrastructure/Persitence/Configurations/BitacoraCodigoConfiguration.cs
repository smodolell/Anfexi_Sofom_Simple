using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class BitacoraCodigoConfiguration : IEntityTypeConfiguration<BitacoraCodigos>
{
    public void Configure(EntityTypeBuilder<BitacoraCodigos> entity)
    {
        entity.HasKey(e => e.IdBitacora).HasFillFactor(90);

        entity.Property(e => e.CodigoCliente).IsUnicode(false);
        entity.Property(e => e.CodigoPromotor).IsUnicode(false);
        entity.Property(e => e.Folio).IsUnicode(false);
        entity.Property(e => e.TelefonoAsesor)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.TelefonoCliente)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.XMLOriginial).IsUnicode(false);

    }

}
