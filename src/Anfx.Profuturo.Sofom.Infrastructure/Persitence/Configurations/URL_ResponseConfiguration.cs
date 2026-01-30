using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class URL_ResponseConfiguration : IEntityTypeConfiguration<URL_Response>
{
    public void Configure(EntityTypeBuilder<URL_Response> entity)
    {
        entity.HasKey(e => e.IdUrlResponse)
            .HasName("PK__URL_Resp__D294B4BE9524FCA7")
            .HasFillFactor(90);

        entity.ToTable("URL_Response");

        entity.HasIndex(e => e.IdUrlRequest, "MejoraDBA02_index_1729064");

        entity.Property(e => e.Error)
            .HasMaxLength(500)
            .IsUnicode(false);
        entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
        entity.Property(e => e.LogSalida).IsUnicode(false);

    }

}
