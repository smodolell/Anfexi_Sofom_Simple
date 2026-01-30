using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> entity)
    {
        entity.HasKey(e => e.IdUsuario).HasFillFactor(90);

        entity.ToTable("Usuario");

        entity.HasIndex(e => e.IdRol, "Mejora_DBA_1141127").HasFillFactor(90);

        entity.HasIndex(e => new { e.Activo, e.IdUnidadNegocio, e.EsGerente, e.IdUsuario }, "Mejora_DBA_242").HasFillFactor(90);

        entity.HasIndex(e => e.NombreCompleto, "Mejora_DBA_30109").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdUnidadNegocio, e.EsGerente, e.IdUsuario }, "Mejora_DBA_33688").HasFillFactor(90);

        entity.HasIndex(e => e.NumeroEmpleado, "Mejora_DBA_37585").HasFillFactor(90);

        entity.HasIndex(e => e.EmployeeID, "Mejora_DBA_99").HasFillFactor(90);

        entity.HasIndex(e => e.UserName, "_dta_index_Usuario_9_450100644__K2_1_3_4_5_6_7_8_9_10_11_12_13_14_16_17_18_19_9987").HasFillFactor(90);

        entity.Property(e => e.Celular)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.Email)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.EmployeeID)
            .HasMaxLength(32)
            .HasDefaultValue("");
        entity.Property(e => e.EsGerente).HasDefaultValue(false);
        entity.Property(e => e.EsPrimerInicio).HasDefaultValue(true);
        entity.Property(e => e.FechaRegistracion).HasColumnType("datetime");
        entity.Property(e => e.IdiomaUI)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.NombreCompleto)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.NumeroEmpleado)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.RequiereCambioPass).HasDefaultValue(false);
        entity.Property(e => e.UserName)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.UserPass)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Usuario)
            .HasForeignKey(d => d.IdGenero)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Usuario_Genero1");

    }

}
