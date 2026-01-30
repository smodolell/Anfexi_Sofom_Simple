using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_TablaAmortizaConfiguration : IEntityTypeConfiguration<COT_TablaAmortiza>
{
    public void Configure(EntityTypeBuilder<COT_TablaAmortiza> entity)
    {
        entity.HasKey(e => e.IdTablaAmortiza)
            .HasName("PK__COT_Tabl__5B3029CF0F582957")
            .HasFillFactor(90);

        entity.ToTable("COT_TablaAmortiza");

        entity.HasIndex(e => e.NoPago, "MejoraDBA02_index_1033186");

        entity.HasIndex(e => new { e.IdCotizador, e.NoPago }, "Mejora_IDX_180334");

        entity.HasIndex(e => e.IdCotizador, "Mejora_IDX_33768");

        entity.HasIndex(e => e.IdCotizador, "Mejora_IDX_33771");

        entity.HasIndex(e => e.IdCotizador, "Mejora_IDX_41");

        entity.Property(e => e.IdTablaAmortiza).ValueGeneratedNever();
        entity.Property(e => e.Capital).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.IVA).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.Interes).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.SaldoFinal).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.SaldoInicial).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.Total).HasColumnType("decimal(13, 6)");

        entity.HasOne(d => d.IdCotizadorNavigation).WithMany(p => p.COT_TablaAmortiza)
            .HasForeignKey(d => d.IdCotizador)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_TablaAmortiza_COT_Cotizador");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<COT_TablaAmortiza> entity);
}
