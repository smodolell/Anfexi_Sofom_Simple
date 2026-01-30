using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> entity)
    {
        entity.HasKey(e => e.IdContrato)
            .HasName("PK__Contrato__8569F05A27F8EE98")
            .HasFillFactor(90);

        entity.ToTable("Contrato");

        entity.HasIndex(e => e.IdEstatusContrato, "IDX_Contrato_EstatusContrato").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdContrato, e.VersionTabla, e.IdTasa, e.IdTipoCredito, e.IdEstatusContrato, e.Contrato1 }, "IDX_Contrato_VersionTabla_EstatusContrato").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdContrato, e.VersionTabla, e.IdTipoCredito }, "IDX_Contrato_VersionTabla_TipoCredito");

        entity.HasIndex(e => e.Contrato1, "IND_Contrato");

        entity.HasIndex(e => new { e.IdEstatusContrato, e.FecActivacion, e.Folio }, "MejoraDBA02_index_1981604").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusContrato, "MejoraDBA02_index_1982720").HasFillFactor(90);

        entity.HasIndex(e => e.IdTipoCredito, "MejoraDBA02_index_1993638");

        entity.HasIndex(e => e.IdEstatusContrato, "Mejora_DBA_1078247").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdEstatusContrato, e.IdTipoCredito, e.FecActivacion, e.SaldoInsoluto }, "Mejora_DBA_1078257").HasFillFactor(90);

        entity.HasIndex(e => e.IdTipoCredito, "Mejora_DBA_1275573");

        entity.HasIndex(e => new { e.IdEstatusContrato, e.SaldoInsoluto }, "Mejora_DBA_1473959").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdEstatusContrato, e.IdTipoCredito, e.SaldoInsoluto }, "Mejora_DBA_1847269").HasFillFactor(90);

        entity.HasIndex(e => e.Folio, "Mejora_IDX_253");

        entity.HasIndex(e => e.IdTipoCredito, "Mejora_IDX_26684");

        entity.HasIndex(e => e.ClabeDeposito, "Mejora_IDX_26707");

        entity.HasIndex(e => e.IdTipoCredito, "Mejora_IDX_314528");

        entity.HasIndex(e => e.FecActivacion, "Mejora_IDX_33797");

        entity.HasIndex(e => new { e.IdTipoCredito, e.IdEstatusContrato }, "Mejora_IDX_351566").HasFillFactor(90);

        entity.HasIndex(e => e.IdSolicitud, "Mejora_IDX_39");

        entity.HasIndex(e => e.IdEstatusContrato, "Mejora_IDX_83").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdTipoCredito, e.IdEstatusContrato }, "Mejora_IDX_92").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusContrato, "Mejora_IDX_94").HasFillFactor(90);

        entity.HasIndex(e => e.Contrato1, "_dta_index_Contrato_9_638625318__K2_1_3_5_37_51_8379");

        entity.HasIndex(e => e.IdPersona, "_dta_index_Contrato_9_638625318__K3").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusContrato, "dba3_index_785").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusContrato, "dba4_index_1132").HasFillFactor(90);

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148174");

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148176");

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148178");

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148277");

        entity.HasIndex(e => new { e.IdTipoCredito, e.FecActivacion, e.SaldoInsoluto }, "dba_index_148405").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdEstatusContrato, e.SaldoInsoluto }, "dba_index_148538").HasFillFactor(90);

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148566");

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_148568");

        entity.HasIndex(e => e.IdEstatusContrato, "dba_index_148638").HasFillFactor(90);

        entity.HasIndex(e => e.IdTipoCredito, "dba_index_149053");

        entity.Property(e => e.IdContrato).ValueGeneratedNever();
        entity.Property(e => e.BallonPayment).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.CAT).HasColumnType("decimal(7, 6)");
        entity.Property(e => e.Capital).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.CapitalFinanciado).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.ClabeDeposito)
            .HasMaxLength(18)
            .IsUnicode(false);
        entity.Property(e => e.ClaveContrato)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.CobranzaComercial)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.Contrato1)
            .HasMaxLength(15)
            .IsUnicode(false)
            .HasColumnName("Contrato");
        entity.Property(e => e.DepositoEnGarantia).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Enganche).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.FactorMora).HasColumnType("decimal(7, 4)");
        entity.Property(e => e.FecActivacion).HasColumnType("datetime");
        entity.Property(e => e.FecCierre)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.FecFinContrato).HasColumnType("datetime");
        entity.Property(e => e.FecInicioContrato).HasColumnType("datetime");
        entity.Property(e => e.FecPrimeraRenta).HasColumnType("datetime");
        entity.Property(e => e.FechaFirmaContrato).HasColumnType("datetime");
        entity.Property(e => e.Folio)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.IdUnidadNegocio).HasDefaultValue(4);
        entity.Property(e => e.MotivoEstatus)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.NroRentasDepositoGarantia).HasColumnType("decimal(13, 4)");
        entity.Property(e => e.OpcionDeCompra).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PorcBallonPayment).HasColumnType("decimal(8, 6)");
        entity.Property(e => e.PorcEnganche).HasColumnType("decimal(8, 6)");
        entity.Property(e => e.PorcOpcionDeCompra).HasColumnType("decimal(8, 6)");
        entity.Property(e => e.PorcValorResidual).HasColumnType("decimal(8, 6)");
        entity.Property(e => e.PuntosMas).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.PuntosMasMora).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.PuntosPor).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.PuntosPorMora).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.SaldoInsoluto).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tasa).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.TasaBase).HasColumnType("decimal(10, 8)");
        entity.Property(e => e.TasaBaseMora).HasColumnType("decimal(10, 6)");
        entity.Property(e => e.TasaIva).HasColumnType("decimal(7, 4)");
        entity.Property(e => e.TasaMora).HasColumnType("decimal(10, 6)");
        entity.Property(e => e.ValorResidual).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.VersionTabla).HasDefaultValue(1);

        entity.HasOne(d => d.IdEstatusContratoNavigation).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.IdEstatusContrato)
            .HasConstraintName("FK_Contrato_EstatusContrato");

        entity.HasOne(d => d.IdPeriodicidadNavigation).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.IdPeriodicidad)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Contrato_SB_Periodicidad");

        entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.IdPersona)
            .HasConstraintName("FK_Contrato_Persona");

        entity.HasOne(d => d.IdTipoCreditoNavigation).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.IdTipoCredito)
            .HasConstraintName("FK_Contrato_TipoCredito");

    }

}
