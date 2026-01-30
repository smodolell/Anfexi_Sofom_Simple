using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class PersonaDireccionConfiguration : IEntityTypeConfiguration<PersonaDireccion>
{
    public void Configure(EntityTypeBuilder<PersonaDireccion> entity)
    {
        entity.HasKey(e => e.IdPersonaDireccion)
            .HasName("PK__PersonaD__D5C9CDF53C34F16F")
            .HasFillFactor(90);

        entity.ToTable("PersonaDireccion");

        entity.HasIndex(e => e.IdPersona, "Mejora_IDX_11").HasFillFactor(90);

        entity.HasIndex(e => e.IdPersona, "Mejora_IDX_201").HasFillFactor(90);

        entity.Property(e => e.IdPersonaDireccion).ValueGeneratedNever();
        entity.Property(e => e.Calle)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.NumExterior)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.NumInterior)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.Pais)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.PersonaDireccion)
            .HasForeignKey(d => d.IdColonia)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PersonaDireccion_Colonia");

        entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.PersonaDireccion)
            .HasForeignKey(d => d.IdPersona)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PersonaDireccion_Persona");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<PersonaDireccion> entity);
}
