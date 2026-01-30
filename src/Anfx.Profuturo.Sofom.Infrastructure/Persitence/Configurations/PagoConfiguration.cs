using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class PagoConfiguration : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> entity)
    {
        entity.HasKey(e => e.IdPago)
            .HasName("PK__Pago__FC851A3A0A688BB1")
            .HasFillFactor(90);

        entity.ToTable("Pago");

        entity.HasIndex(e => new { e.Estatus, e.IdTipoPago, e.MontoPago }, "MejoraDBA02_index_1966962").HasFillFactor(90);

        entity.HasIndex(e => e.Estatus, "MejoraDBA02_index_1974809");

        entity.HasIndex(e => e.Estatus, "MejoraDBA02_index_1978168");

        entity.HasIndex(e => e.IdTipoPago, "Mejora_DBA_1917304");

        entity.HasIndex(e => new { e.IdTipoPago, e.Contrato, e.Estatus }, "Mejora_IDX_692572").HasFillFactor(90);

        entity.HasIndex(e => new { e.Contrato, e.Estatus }, "Mejora_IDX_692835").HasFillFactor(90);

        entity.HasIndex(e => e.Contrato, "Mejora_IDX_714387").HasFillFactor(90);

        entity.HasIndex(e => e.Contrato, "Mejora_IDX_718057").HasFillFactor(90);

        entity.HasIndex(e => new { e.SaldoPago, e.Estatus, e.Contrato }, "_dta_index_Pago_9_142623551__K10_K12_K5_1_2_3_4_6_7_8_9_11_13_14_9987_4364").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdPersona, e.SaldoPago, e.Estatus }, "_dta_index_Pago_9_142623551__K4_K10_K12_1_2_3_5_6_7_8_9_11_13_14_9987_4364").HasFillFactor(90);

        entity.HasIndex(e => e.FecPagoRegistro, "dba3_index_1104");

        entity.HasIndex(e => e.FecPagoValor, "dba3_index_985");

        entity.HasIndex(e => new { e.Contrato, e.Estatus, e.SaldoPago }, "dba_index_149058").HasFillFactor(90);

        entity.Property(e => e.IdPago).ValueGeneratedNever();
        entity.Property(e => e.Contrato)
            .IsRequired()
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.Estatus).HasDefaultValue(false);
        entity.Property(e => e.FecPagoRegistro).HasColumnType("datetime");
        entity.Property(e => e.FecPagoValor).HasColumnType("datetime");
        entity.Property(e => e.FecUltimoCambio).HasColumnType("datetime");
        entity.Property(e => e.MontoPago).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoPagoAplicado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.ReferenciaNumerica)
            .IsRequired()
            .HasMaxLength(8)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.SaldoPago)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Suspenso).HasDefaultValue(false);

        
    }

}
