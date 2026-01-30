using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_SolicitanteDireccionConfiguration : IEntityTypeConfiguration<OT_SolicitanteDireccion>
{
    public void Configure(EntityTypeBuilder<OT_SolicitanteDireccion> entity)
    {
        entity.HasKey(e => e.IdDireccion).HasFillFactor(90);

        entity.ToTable("OT_SolicitanteDireccion");

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_27577");

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_714376");

        entity.Property(e => e.IdDireccion).ValueGeneratedNever();
        entity.Property(e => e.Calle)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.EntreCalleDos)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.EntreCalleUno)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NumExterior)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.NumInterior)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.Pais)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.RentaMensual).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.TipoDomicilio)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.OT_SolicitanteDireccion)
            .HasForeignKey(d => d.IdColonia)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_SolicitanteDireccion_Colonia");

        entity.HasOne(d => d.IdSolicitanteNavigation).WithMany(p => p.OT_SolicitanteDireccion)
            .HasForeignKey(d => d.IdSolicitante)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_SolicitanteDireccion_OT_Solicitante");

    }

}
