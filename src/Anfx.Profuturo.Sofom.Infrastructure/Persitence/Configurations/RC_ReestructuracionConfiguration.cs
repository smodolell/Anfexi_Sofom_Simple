using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class RC_ReestructuracionConfiguration : IEntityTypeConfiguration<RC_Reestructuracion>
{
    public void Configure(EntityTypeBuilder<RC_Reestructuracion> entity)
    {
        entity.HasKey(e => e.IdReestructuracion)
            .HasName("PK__RC_Reest__3B93FDA51C7D1A4B")
            .HasFillFactor(90);

        entity.ToTable("RC_Reestructuracion");

        entity.HasIndex(e => e.IdSolicitudNew, "IX_RC_Restructuracion_220225");

        entity.HasIndex(e => e.IdEstatusSolicitud, "MejoraDBA02_index_1974687");

        entity.HasIndex(e => e.IdAgencia, "MejoraDBA02_index_1985936");

        entity.HasIndex(e => new { e.IdSolicitud, e.IdSolicitudNew }, "Mejora_DBA_1078908").HasFillFactor(90);

        entity.HasIndex(e => e.IdContratoNew, "Mejora_DBA_1191121");

        entity.HasIndex(e => new { e.IdContrato, e.Activo }, "Mejora_IDX_180").HasFillFactor(90);

        entity.HasIndex(e => new { e.IdContrato, e.IdContratoNew }, "Mejora_IDX_56194").HasFillFactor(90);

        entity.HasIndex(e => e.IdEstatusSolicitud, "dba3_index_520");

        entity.HasIndex(e => e.SaldoInsoluto, "dba3_index_543");

        entity.HasIndex(e => e.Activo, "dba_index_148193");

        entity.HasIndex(e => new { e.IdSolicitudNew, e.FechaInicio }, "dba_index_148426");

        entity.HasIndex(e => new { e.IdSolicitudNew, e.FechaInicio }, "dba_index_148428");

        entity.Property(e => e.Capital).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.CapitalALibrerar).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.CapitalNew).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.CapitalPagado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(12, 2)");
        entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
        entity.Property(e => e.FechaFirmaContrato)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.FechaInicio).HasColumnType("datetime");
        entity.Property(e => e.FechaPrimeraRenta)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.LiberaCapital).HasDefaultValue(false);
        entity.Property(e => e.MontoDepositoRes).HasColumnType("decimal(15, 2)");
        entity.Property(e => e.MontoDisponible).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.PagoFijoTotalAproximado)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(12, 2)");
        entity.Property(e => e.PagoFijoTotalAproximadoMensual)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(12, 2)");
        entity.Property(e => e.SaldoInsoluto).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.Tasa).HasColumnType("decimal(12, 8)");
        entity.Property(e => e.TasaNew).HasColumnType("decimal(12, 8)");

        entity.HasOne(d => d.IdAgenciaNavigation).WithMany(p => p.RC_Reestructuracion)
            .HasForeignKey(d => d.IdAgencia)
            .HasConstraintName("FK__RC_Reestr__IdAge__2235F3A1");

        entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.RC_ReestructuracionIdContratoNavigation)
            .HasForeignKey(d => d.IdContrato)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RC_Reestr__IdCon__204DAB2F");

        entity.HasOne(d => d.IdContratoNewNavigation).WithMany(p => p.RC_ReestructuracionIdContratoNewNavigation)
            .HasForeignKey(d => d.IdContratoNew)
            .HasConstraintName("FK__RC_Reestr__IdCon__2141CF68");

        entity.HasOne(d => d.IdPeriodicidadNavigation).WithMany(p => p.RC_ReestructuracionIdPeriodicidadNavigation)
            .HasForeignKey(d => d.IdPeriodicidad)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RC_Reestr__IdPer__26068485");

        entity.HasOne(d => d.IdPeriodicidadNewNavigation).WithMany(p => p.RC_ReestructuracionIdPeriodicidadNewNavigation)
            .HasForeignKey(d => d.IdPeriodicidadNew)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RC_Reestr__IdPer__2BBF5DDB");

        entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.RC_ReestructuracionIdPlanNavigation)
            .HasForeignKey(d => d.IdPlan)
            .HasConstraintName("FK__RC_Reestr__IdPla__26FAA8BE");

        entity.HasOne(d => d.IdPlanNewNavigation).WithMany(p => p.RC_ReestructuracionIdPlanNewNavigation)
            .HasForeignKey(d => d.IdPlanNew)
            .HasConstraintName("FK__RC_Reestr__IdPla__27EECCF7");

        entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.RC_Reestructuracion)
            .HasForeignKey(d => d.IdPlazo)
            .HasConstraintName("FK__RC_Reestr__IdPla__29D71569");

        entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.RC_ReestructuracionIdSolicitudNavigation)
            .HasForeignKey(d => d.IdSolicitud)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__RC_Reestr__IdSol__1E6562BD");

        entity.HasOne(d => d.IdSolicitudNewNavigation).WithMany(p => p.RC_ReestructuracionIdSolicitudNewNavigation)
            .HasForeignKey(d => d.IdSolicitudNew)
            .HasConstraintName("FK__RC_Reestr__IdSol__1F5986F6");

    }

}
