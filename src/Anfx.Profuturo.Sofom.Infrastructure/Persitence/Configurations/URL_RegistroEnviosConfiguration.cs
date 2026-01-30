using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class URL_RegistroEnviosConfiguration : IEntityTypeConfiguration<URL_RegistroEnvios>
{
    public void Configure(EntityTypeBuilder<URL_RegistroEnvios> entity)
    {
        entity.HasKey(e => e.IdRegistroEnvio)
            .HasName("PK__URL_Regi__F8DBBC1F53CE6A59")
            .HasFillFactor(90);

        entity.Property(e => e.Code)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.Data).IsUnicode(false);
        entity.Property(e => e.Error)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.Folio)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.Success)
            .HasMaxLength(150)
            .IsUnicode(false);

    }

}
