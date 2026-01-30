using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class PersonaFisicaConfiguration : IEntityTypeConfiguration<PersonaFisica>
{
    public void Configure(EntityTypeBuilder<PersonaFisica> entity)
    {
        entity.HasKey(e => e.IdPersona)
            .HasName("PK__PersonaF__2EC8D2AC37703C52")
            .HasFillFactor(90);

        entity.ToTable("PersonaFisica");

        entity.Property(e => e.IdPersona).ValueGeneratedNever();
        entity.Property(e => e.CURP)
            .HasMaxLength(18)
            .IsUnicode(false);
        entity.Property(e => e.Sexo)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();

        entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.PersonaFisica)
            .HasForeignKey<PersonaFisica>(d => d.IdPersona)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PersonaFisica_Persona");

    }


}
