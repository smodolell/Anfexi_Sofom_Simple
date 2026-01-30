using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_SolicitantePFCuentaBancariaConfiguration : IEntityTypeConfiguration<OT_SolicitantePFCuentaBancaria>
{
    public void Configure(EntityTypeBuilder<OT_SolicitantePFCuentaBancaria> entity)
    {
        entity.HasKey(e => e.IdCuentaBancaria)
            .HasName("PK__OT_Solic__D6CD6A7D546C6DB6")
            .HasFillFactor(90);

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_32511");

        entity.HasIndex(e => e.IdSolicitante, "Mejora_IDX_33706");

        entity.Property(e => e.IdCuentaBancaria).ValueGeneratedNever();
        entity.Property(e => e.Banco)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.CuentaBancaria)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.CuentaClabe)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.FechaApertura).HasColumnType("datetime");
        entity.Property(e => e.IdBanco).HasDefaultValue(0);
        entity.Property(e => e.IdTipoCuenta).HasDefaultValue(2, "DF__OT_Solici__IdTip__08CB2759");
        entity.Property(e => e.NumeroTarjeta)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.SaldoPromedio).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.TipoCuenta)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.OT_SolicitantePFCuentaBancaria)
            .HasForeignKey(d => d.IdBanco)
            .HasConstraintName("fk_banco");

        entity.HasOne(d => d.IdSolicitanteNavigation).WithMany(p => p.OT_SolicitantePFCuentaBancaria)
            .HasForeignKey(d => d.IdSolicitante)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_SolicitantePFCuentaBancaria_OT_Solicitante");

    }

}
