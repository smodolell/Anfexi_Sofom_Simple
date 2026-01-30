using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_DocumentoConfigConfiguration : IEntityTypeConfiguration<EXP_DocumentoConfig>
{
    public void Configure(EntityTypeBuilder<EXP_DocumentoConfig> entity)
    {
        entity.HasKey(e => e.IdDocumentoConfig);

        entity.ToTable("EXP_DocumentoConfig");

        entity.Property(e => e.IdDocumentoConfig).ValueGeneratedNever();
        entity.Property(e => e.Referencia)
            .HasMaxLength(100)
            .IsUnicode(false);

        
    }

}
