using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class OT_FaseHistoriaConfiguration : IEntityTypeConfiguration<OT_FaseHistoria>
    {
        public void Configure(EntityTypeBuilder<OT_FaseHistoria> entity)
        {
            entity.HasKey(e => e.IdFaseHistoria).HasFillFactor(90);

            entity.HasIndex(e => new { e.IdFase, e.IdEstatusFase, e.FechaUltimaModificacion }, "MejoraDBA02_index_1132039");

            entity.HasIndex(e => new { e.IdSolicitud, e.IdFase }, "_dta_index_OT_FaseHistoria_9_1862297694__K3_K2_1").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdSolicitud, e.IdFase, e.Activo }, "_dta_index_OT_FaseHistoria_9_1862297694__K3_K2_K10_1_4_6_7_8_9_8066").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdFase, e.IdEstatusFase, e.Activo }, "dba_index_143971");

            entity.HasIndex(e => new { e.IdEstatusFase, e.Activo }, "dba_index_143973");

            entity.Property(e => e.IdFaseHistoria).ValueGeneratedNever();
            entity.Property(e => e.Comentario)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.FechaEntrada).HasColumnType("datetime");
            entity.Property(e => e.FechaUltimaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdEstatusFaseNavigation).WithMany(p => p.OT_FaseHistoria)
                .HasForeignKey(d => d.IdEstatusFase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OT_FaseHistoria_OT_EstatusFase");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.OT_FaseHistoria)
                .HasForeignKey(d => d.IdSolicitud)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OT_FaseHistoria_OT_Solicitud");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OT_FaseHistoria)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OT_FaseHistoria_Usuario");

     
        }

    }
}
