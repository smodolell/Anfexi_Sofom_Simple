using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class URL_SolicitudConfiguration : IEntityTypeConfiguration<URL_Solicitud>
{
    public void Configure(EntityTypeBuilder<URL_Solicitud> entity)
    {
        entity.HasKey(e => e.IdUrlSolicitud)
            .HasName("PK__URL_Soli__E8651F3951AEC3FB")
            .HasFillFactor(90);

        entity.ToTable("URL_Solicitud");

        entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

    }

}
