using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RC_ProcesoEstadoConfiguration : IEntityTypeConfiguration<RC_ProcesoEstado>
{
    public void Configure(EntityTypeBuilder<RC_ProcesoEstado> entity)
    {
        entity.HasKey(e => e.IdEstadoProceso)
            .HasName("PK__RC_Proce__4AFB8C7E42A2C333")
            .HasFillFactor(90);

        entity.ToTable("RC_ProcesoEstado");

        entity.Property(e => e.IdEstadoProceso).ValueGeneratedNever();
        entity.Property(e => e.EstadoProceso)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Icono)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

    }

}
