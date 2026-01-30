using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> entity)
        {
            entity.HasKey(e => e.IdMovimiento)
                .HasName("PK__Movimien__881A6AE03BFFE745")
                .HasFillFactor(90);

            entity.ToTable("Movimiento");

            entity.HasIndex(e => e.FecMovimiento, "IDC_Mov_IdMov_NoPago_SaldoT_IdCon").HasFillFactor(90);

            entity.HasIndex(e => new { e.FecMovimiento, e.SaldoTotal }, "IND_FecMovimiento2");

            entity.HasIndex(e => new { e.IdMovimiento, e.SaldoTotal }, "IND_FechaMovimiento");

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.IdContrato, e.SaldoTotal }, "IND_Movimiento3").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.FecMovimiento, e.SaldoTotal }, "IND_Movimiento4").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.SaldoTotal }, "IND_TipoMovimiento");

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.SaldoTotal }, "MejoraDBA02_index_1976972");

            entity.HasIndex(e => new { e.IdContrato, e.IdTipoMovimiento }, "MejoraDBA02_index_1977063").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.IdTipoMovimiento }, "MejoraDBA02_index_1977065").HasFillFactor(90);

            entity.HasIndex(e => new { e.Total, e.SaldoTotal, e.FecUltimoCambio }, "MejoraDBA02_index_1980742").HasFillFactor(90);

            entity.HasIndex(e => e.Descripcion, "Mejora_DBA_1914681");

            entity.HasIndex(e => e.Descripcion, "Mejora_DBA_1914684");

            entity.HasIndex(e => new { e.IdContrato, e.IdMovimiento, e.FecMovimiento, e.IdTipoMovimiento }, "_dta_index_Movimiento_9_974626515__K16_K1_K6_K2_3_4_5_7_8_9_10_11_12_13_14_15_1912").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdPersona, e.FecMovimiento }, "_dta_index_Movimiento_9_974626515__K3_K6_14_16").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdPersona, e.FecMovimiento, e.IdTipoMovimiento }, "_dta_index_Movimiento_9_974626515__K3_K6_K2_1_4_5_7_8_9_10_11_12_13_14_15_16_8066").HasFillFactor(90);

            entity.HasIndex(e => new { e.SaldoTotal, e.IdContrato }, "dba3_index_1041").HasFillFactor(90);

            entity.HasIndex(e => new { e.SaldoTotal, e.IdContrato }, "dba3_index_157").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.SaldoTotal }, "dba3_index_509").HasFillFactor(90);

            entity.HasIndex(e => new { e.NoPago, e.IdContrato }, "dba3_index_535").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdPersona, e.IdTipoMovimiento, e.SaldoTotal }, "dba3_index_566").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.NoPago }, "dba3_index_678").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.NoPago, e.IdContrato }, "dba3_index_859");

            entity.HasIndex(e => new { e.IdContrato, e.SaldoTotal }, "dba3_index_861").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.SaldoTotal }, "dba3_index_920").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.IdContrato }, "dba3_index_927").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdTipoMovimiento, e.IdContrato, e.FecMovimiento }, "dba3_index_931").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.SaldoTotal }, "dba3_index_963").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.SaldoTotal }, "dba3_index_980").HasFillFactor(90);

            entity.HasIndex(e => new { e.IdContrato, e.FecMovimiento, e.SaldoTotal }, "dba3_index_982").HasFillFactor(90);

            entity.HasIndex(e => e.SaldoTotal, "dba_index_148547");

            entity.Property(e => e.IdMovimiento).ValueGeneratedNever();
            entity.Property(e => e.Capital)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FecMovimiento).HasColumnType("datetime");
            entity.Property(e => e.FecUltimoCambio).HasColumnType("datetime");
            entity.Property(e => e.IVA)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Interes)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.SaldoCapital)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.SaldoIVA)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.SaldoInteres)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.SaldoTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(13, 2)");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.Movimiento)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("FK_Movimiento_Contrato");

        }

    }
}
