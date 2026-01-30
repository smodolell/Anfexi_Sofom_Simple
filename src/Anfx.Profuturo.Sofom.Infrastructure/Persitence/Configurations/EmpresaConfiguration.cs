using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> entity)
    {
        entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033E2E1BDC42");

        entity.ToTable("Empresa");

        entity.Property(e => e.IdEmpresa).ValueGeneratedNever();
        entity.Property(e => e.AclaracionesEstadodeCuenta).HasColumnType("text");
        entity.Property(e => e.AdvertenciasEstadodeCuenta).HasColumnType("text");
        entity.Property(e => e.AvisosEstadodeCuenta).HasColumnType("text");
        entity.Property(e => e.DireccionEmpresa)
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.Empresa1)
            .HasMaxLength(180)
            .IsUnicode(false)
            .HasColumnName("Empresa");
        entity.Property(e => e.RFC)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.RazonSocial)
            .HasMaxLength(180)
            .IsUnicode(false);
        entity.Property(e => e.Representante)
            .HasMaxLength(150)
            .IsUnicode(false);
        entity.Property(e => e.Telefono)
            .HasMaxLength(12)
            .IsUnicode(false);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Empresa> entity);
}
