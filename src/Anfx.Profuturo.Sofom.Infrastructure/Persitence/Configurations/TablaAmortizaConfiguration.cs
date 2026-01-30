using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class TablaAmortizaConfiguration : IEntityTypeConfiguration<TablaAmortiza>
{
    public void Configure(EntityTypeBuilder<TablaAmortiza> entity)
    {
        entity.HasKey(e => e.IdTablaAmortiza)
            .HasName("PK__TablaAmo__5B3029CF43A1090D")
            .HasFillFactor(90);

        entity.ToTable("TablaAmortiza");

        entity.HasIndex(e => new { e.IdContrato, e.VersionTabla }, "IND_TablaAmortiza");

        entity.HasIndex(e => e.VersionTabla, "Mejora_IDX_26768");

        entity.HasIndex(e => e.NoPago, "Mejora_IDX_29265");

        entity.HasIndex(e => e.NoPago, "Mejora_IDX_314396");

        entity.HasIndex(e => e.IdContrato, "Mejora_IDX_400994");

        entity.HasIndex(e => new { e.IdContrato, e.VersionTabla, e.Procesado }, "Mejora_IDX_402206").HasFillFactor(90);

        entity.HasIndex(e => e.IdContrato, "Mejora_IDX_419407");

        entity.HasIndex(e => new { e.IdTipoTabla, e.IdContrato, e.VersionTabla }, "Mejora_IDX_419416");

        entity.HasIndex(e => new { e.IdContrato, e.VersionTabla }, "_dta_index_TablaAmortiza_9_1102626971__K3_K15_4149");

        entity.HasIndex(e => new { e.NoPago, e.VersionTabla }, "dba_index_1403");

        entity.HasIndex(e => new { e.IdTipoTabla, e.IdContrato }, "dba_index_146137");

        entity.HasIndex(e => new { e.IdTipoTabla, e.IdContrato, e.Procesado }, "dba_index_146139").HasFillFactor(90);

        entity.Property(e => e.IdTablaAmortiza).ValueGeneratedNever();
        entity.Property(e => e.Capital).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.EsValorResidual).HasDefaultValue(false);
        entity.Property(e => e.FecFinal).HasColumnType("datetime");
        entity.Property(e => e.FecInicial).HasColumnType("datetime");
        entity.Property(e => e.FecVencimiento).HasColumnType("datetime");
        entity.Property(e => e.IVA).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.Interes).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.SaldoFinal).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.SaldoInicial).HasColumnType("decimal(13, 6)");
        entity.Property(e => e.Total).HasColumnType("decimal(13, 6)");

        entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.TablaAmortiza)
            .HasForeignKey(d => d.IdContrato)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TablaAmortiza_Contrato");

        
    }

}
