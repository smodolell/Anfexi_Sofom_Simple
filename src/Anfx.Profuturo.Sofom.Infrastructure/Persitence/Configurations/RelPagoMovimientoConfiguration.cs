using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RelPagoMovimientoConfiguration : IEntityTypeConfiguration<RelPagoMovimiento>
{
    public void Configure(EntityTypeBuilder<RelPagoMovimiento> entity)
    {
        entity.HasKey(e => e.IdPagoMovimiento)
            .HasName("PK__RelPagoM__A8B827845B78929E")
            .HasFillFactor(90);

        entity.ToTable("RelPagoMovimiento");

        entity.HasIndex(e => e.IdPago, "_dta_index_RelPagoMovimiento_9_1502628396__K2_1_3_4_5_6_7_8_9_10_11_12_13_9987_4364");

        entity.HasIndex(e => new { e.IdMovimiento, e.Estatus, e.IdPago }, "_dta_index_RelPagoMovimiento_9_1502628396__K3_K9_K2_8_1912");

        entity.Property(e => e.IdPagoMovimiento).ValueGeneratedNever();
        entity.Property(e => e.CapitalPagado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.CausaCancelacion)
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.Estatus).HasDefaultValue(false);
        entity.Property(e => e.FecAplicacion).HasColumnType("datetime");
        entity.Property(e => e.FecCancelacion).HasColumnType("datetime");
        entity.Property(e => e.FecUltimoCambio).HasColumnType("datetime");
        entity.Property(e => e.IVAPagado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.InteresPagado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Reaplicado).HasDefaultValue(false);
        entity.Property(e => e.TotalPagado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");

        entity.HasOne(d => d.IdMovimientoNavigation).WithMany(p => p.RelPagoMovimiento)
            .HasForeignKey(d => d.IdMovimiento)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_RelPagoMovimiento_Movimiento");

        entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.RelPagoMovimiento)
            .HasForeignKey(d => d.IdPago)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_RelPagoMovimiento_Pago");

    }

}
