using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class TipoCreditoConfiguration : IEntityTypeConfiguration<TipoCredito>
{
    public void Configure(EntityTypeBuilder<TipoCredito> entity)
    {
        entity.HasKey(e => e.IdTipoCredito).HasName("PK__TipoCred__24CBCF7A1209AD79");

        entity.ToTable("TipoCredito");

        entity.Property(e => e.IdTipoCredito).ValueGeneratedNever();
        entity.Property(e => e.BUSINESS_UNIT)
            .IsRequired()
            .HasMaxLength(5)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.CargoCheque).HasColumnType("decimal(7, 4)");
        entity.Property(e => e.ClaveTipoCredito)
            .HasMaxLength(5)
            .IsUnicode(false);
        entity.Property(e => e.EnvioFad).HasDefaultValue(false);
        entity.Property(e => e.FactorGastoCob).HasColumnType("decimal(13, 4)");
        entity.Property(e => e.FactorVentaPublico).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MaxMontoGastoCob).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoKilometraje)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoPenaNoDevuelto).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PorcCancelacionCheque).HasColumnType("decimal(7, 4)");
        entity.Property(e => e.Postfijo)
            .HasMaxLength(5)
            .IsUnicode(false);
        entity.Property(e => e.Prefijo)
            .HasMaxLength(5)
            .IsUnicode(false);
        entity.Property(e => e.Prefijo_Cert)
            .HasMaxLength(2)
            .IsUnicode(false);
        entity.Property(e => e.Prefijo_Reemb)
            .HasMaxLength(2)
            .IsUnicode(false);
        entity.Property(e => e.SUB_Tipo_CIF)
            .HasMaxLength(64)
            .IsUnicode(false);
        entity.Property(e => e.SubTipo_Cert)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.SubTipo_Reemb)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.TasaMoraDefault).HasColumnType("decimal(10, 6)");
        entity.Property(e => e.TipoCredito1)
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("TipoCredito");
        entity.Property(e => e.Tipo_Reemb)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.dMontoParaCertificar).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.sSUB_Tipo_CIF_CERT)
            .HasMaxLength(84)
            .IsUnicode(false);

        entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TipoCredito)
            .HasForeignKey(d => d.IdEmpresa)
            .HasConstraintName("FK_TipoCredito_Empresa");

    }

}
