using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class URL_RequestConfiguration : IEntityTypeConfiguration<URL_Request>
{
    public void Configure(EntityTypeBuilder<URL_Request> entity)
    {
        entity.HasKey(e => e.IdUrlRequest)
            .HasName("PK__URL_Requ__89E9F3DEEA664032")
            .HasFillFactor(90);

        entity.ToTable("URL_Request");

        entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
        entity.Property(e => e.LogEntrada).IsUnicode(false);
        entity.Property(e => e.Metodo)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.token)
            .HasMaxLength(100)
            .IsUnicode(false);

    }
}
