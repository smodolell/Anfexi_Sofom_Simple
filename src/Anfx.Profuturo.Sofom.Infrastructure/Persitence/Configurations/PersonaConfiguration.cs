using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> entity)
    {
        entity.HasKey(e => e.IdPersona)
            .HasName("PK__Persona__2EC8D2AC014935CB")
            .HasFillFactor(90);

        entity.ToTable("Persona");

        entity.HasIndex(e => e.NSS, "Mejora_DBA_1070530").HasFillFactor(90);

        entity.HasIndex(e => e.Email, "Mejora_DBA_1155267").HasFillFactor(90);

        entity.HasIndex(e => new { e.NombrePersona, e.ApellidoPaterno, e.ApellidoMaterno }, "Mejora_DBA_1189810").HasFillFactor(90);

        entity.HasIndex(e => new { e.ApellidoPaterno, e.ApellidoMaterno }, "Mejora_DBA_1189813").HasFillFactor(90);

        entity.HasIndex(e => e.ApellidoPaterno, "Mejora_DBA_1189829").HasFillFactor(90);

        entity.HasIndex(e => e.NoEmpleadoCliente, "Mejora_DBA_1280661").HasFillFactor(90);

        entity.HasIndex(e => e.CURP, "Mejora_IDX_158").HasFillFactor(90);

        entity.HasIndex(e => e.RFC, "Mejora_IDX_178").HasFillFactor(90);

        entity.HasIndex(e => e.RFC, "dba_index_148868").HasFillFactor(90);

        entity.Property(e => e.IdPersona).ValueGeneratedNever();
        entity.Property(e => e.ApellidoMaterno)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.ApellidoPaterno)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.CURP)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.Cuenta)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.CuentaClabe)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.Email)
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.EmpresaCliente)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.Fecha).HasColumnType("datetime");
        entity.Property(e => e.FechaAlta).HasColumnType("datetime");
        entity.Property(e => e.IdEntidadFederativa)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.IdGenero).HasDefaultValueSql("('')");
        entity.Property(e => e.IdUnidadNegocio).HasDefaultValue(4);
        entity.Property(e => e.Identificador)
            .HasMaxLength(16)
            .IsUnicode(false);
        entity.Property(e => e.MontoPensionOrfandad).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Nacionalidad)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.NoEmpleadoCliente)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.NombrePersona)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.NumeroSerieElectronica)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.OcupacionOGiro)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.PaisNacimiento)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.PreguntaEstadoCuenta)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.RFC)
            .HasMaxLength(13)
            .IsUnicode(false);
        entity.Property(e => e.TipoNomina)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.TipoPension)
            .HasMaxLength(100)
            .IsUnicode(false);

    }

}
