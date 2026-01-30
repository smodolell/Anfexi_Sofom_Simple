using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class COT_PlanPlazoConfiguration : IEntityTypeConfiguration<COT_PlanPlazo>
    {
        public void Configure(EntityTypeBuilder<COT_PlanPlazo> entity)
        {
            entity.HasKey(e => new { e.IdPlan, e.IdPlazo })
                .HasName("PK_COT_PlanPlazo_1")
                .HasFillFactor(90);

            entity.ToTable("COT_PlanPlazo");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.COT_PlanPlazo)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COT_PlanPlazo_COT_Plan");

            entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.COT_PlanPlazo)
                .HasForeignKey(d => d.IdPlazo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COT_PlanPlazo_COT_Plazo");

        }

    }
}
