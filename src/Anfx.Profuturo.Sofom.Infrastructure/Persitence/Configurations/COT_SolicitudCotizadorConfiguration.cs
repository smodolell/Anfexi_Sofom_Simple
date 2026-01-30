using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_SolicitudCotizadorConfiguration : IEntityTypeConfiguration<COT_SolicitudCotizador>
{
    public void Configure(EntityTypeBuilder<COT_SolicitudCotizador> entity)
    {
        entity.HasKey(e => new { e.IdSolicitud, e.IdCotizador }).HasFillFactor(90);

        entity.ToTable("COT_SolicitudCotizador");

        entity.HasIndex(e => e.IdCotizador, "Mejora_DBA_33721");

        entity.HasIndex(e => e.IdContrato, "index_COT_SolicitudCotizadorContrato");

        entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.COT_SolicitudCotizador)
            .HasForeignKey(d => d.IdContrato)
            .HasConstraintName("FK_COT_SolicitudCotizador_Contrato");

        entity.HasOne(d => d.IdCotizadorNavigation).WithMany(p => p.COT_SolicitudCotizador)
            .HasForeignKey(d => d.IdCotizador)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_SolicitudCotizador_COT_Cotizador1");

        entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.COT_SolicitudCotizador)
            .HasForeignKey(d => d.IdSolicitud)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_SolicitudCotizador_OT_Solicitud");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<COT_SolicitudCotizador> entity);
}
