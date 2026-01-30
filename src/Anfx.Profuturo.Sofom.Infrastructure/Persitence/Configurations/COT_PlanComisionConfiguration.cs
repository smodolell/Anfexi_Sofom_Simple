using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_PlanComisionConfiguration : IEntityTypeConfiguration<COT_PlanComision>
{
    public void Configure(EntityTypeBuilder<COT_PlanComision> entity)
    {
        entity.HasKey(e => new { e.IdPlan, e.IdPlazo, e.IdComision }).HasFillFactor(90);

        entity.ToTable("COT_PlanComision");

        entity.Property(e => e.VariacionMaxima).HasColumnType("decimal(9, 4)");
        entity.Property(e => e.VariacionMinima).HasColumnType("decimal(9, 4)");

        entity.HasOne(d => d.IdComisionNavigation).WithMany(p => p.COT_PlanComision)
            .HasForeignKey(d => d.IdComision)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_PlanComision_COT_Comision");

        entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.COT_PlanComision)
            .HasForeignKey(d => d.IdPlan)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_PlanComision_COT_Plan");

        entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.COT_PlanComision)
            .HasForeignKey(d => d.IdPlazo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_PlanComision_COT_Plazo");

    }

}
