using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_AgenciaConfiguration : IEntityTypeConfiguration<COT_Agencia>
{
    public void Configure(EntityTypeBuilder<COT_Agencia> entity)
    {
        entity.HasKey(e => e.IdAgencia).HasName("PK__Agencia__00863C7D18A19C6F");

        entity.Property(e => e.IdAgencia).ValueGeneratedNever();
        entity.Property(e => e.ApellidoMaterno)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.ApellidoPaterno)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.Calle)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.Email)
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.Extension)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Fecha).HasColumnType("datetime");
        entity.Property(e => e.FechaAlta).HasColumnType("datetime");
        entity.Property(e => e.IdClientePeopleSoft)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.Identificador)
            .HasMaxLength(16)
            .IsUnicode(false);
        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.NombreContacto)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.NombrePersona)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.NumExterior)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.NumInterior)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.RFC)
            .HasMaxLength(13)
            .IsUnicode(false);
        entity.Property(e => e.ReferenciaCIF)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Representante)
            .IsRequired()
            .HasMaxLength(180)
            .IsUnicode(false)
            .HasDefaultValue("", "DF_COT_Agencia_Representante");
        entity.Property(e => e.Telefono)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.COT_Agencia)
            .HasForeignKey(d => d.IdColonia)
            .HasConstraintName("FK_COT_Agencia_Colonia");

    }

}
