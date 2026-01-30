using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_PlanConfiguration : IEntityTypeConfiguration<COT_Plan>
{
    public void Configure(EntityTypeBuilder<COT_Plan> entity)
    {
        entity.HasKey(e => e.IdPlan).HasFillFactor(90);

        entity.ToTable("COT_Plan");

        entity.Property(e => e.IdPlan).ValueGeneratedNever();
        entity.Property(e => e.CesantíaEdadAvanzada).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.ClaveNomina)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.Comision).HasColumnType("decimal(9, 4)");
        entity.Property(e => e.Descripcion)
            .HasMaxLength(500)
            .IsUnicode(false);
        entity.Property(e => e.FechaVigenciaFin).HasColumnType("datetime");
        entity.Property(e => e.FechaVigenciaIni).HasColumnType("datetime");
        entity.Property(e => e.ImporteMaximo).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.ImporteMinimo).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Interes).HasColumnType("decimal(9, 4)");
        entity.Property(e => e.Invalidez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoDescuento).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoEdadMaxima).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoMinimoIngreso).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoMinimoPension).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.MontoReestructura).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.NombrePlan)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.PH_CesantiaEdadAvanzada).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Invalidez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_RetiroAnticipado).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Tipo1).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Tipo2).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Tipo3).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Tipo4).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Vejez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_Viudez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PH_ViudezOrfandad).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.RangoEnganche).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.RangoMantenimiento).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.RangoValorResidual).HasColumnType("decimal(9, 6)");
        entity.Property(e => e.RetiroAnticipado).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tipo1).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tipo2).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tipo3).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tipo4).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Vejez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Viudez).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.ViudezOrfandad).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.porcentajeDescuento).HasColumnType("decimal(8, 2)");

        entity.HasOne(d => d.IdTipoCreditoNavigation).WithMany(p => p.COT_Plan)
            .HasForeignKey(d => d.IdTipoCredito)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_Plan_TipoCredito");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<COT_Plan> entity);
}
