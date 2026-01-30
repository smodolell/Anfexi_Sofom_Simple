using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_SolicitanteConfiguration : IEntityTypeConfiguration<OT_Solicitante>
{
    public void Configure(EntityTypeBuilder<OT_Solicitante> entity)
    {
        entity.HasKey(e => e.IdSolicitante).HasFillFactor(90);

        entity.ToTable("OT_Solicitante");

        entity.HasIndex(e => e.Email, "Mejora_DBA_156859").HasFillFactor(90);

        entity.HasIndex(e => e.RFC, "dba3_index_3").HasFillFactor(90);

        entity.Property(e => e.IdSolicitante).ValueGeneratedNever();
        entity.Property(e => e.Email)
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
        entity.Property(e => e.Nombres_RazonSocial)
            .IsRequired()
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.RFC)
            .HasMaxLength(18)
            .IsUnicode(false);

    }


}
