using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class COT_PlanTasaConfiguration : IEntityTypeConfiguration<COT_PlanTasa>
    {
        public void Configure(EntityTypeBuilder<COT_PlanTasa> entity)
        {
            entity.HasKey(e => new { e.IdPlan, e.IdTasa, e.IdPlazo }).HasFillFactor(90);

            entity.ToTable("COT_PlanTasa");

            entity.Property(e => e.VariacionMaxima).HasColumnType("decimal(9, 4)");
            entity.Property(e => e.VariacionMinima).HasColumnType("decimal(9, 4)");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.COT_PlanTasa)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COT_PlanTasa_COT_Plan");

            entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.COT_PlanTasa)
                .HasForeignKey(d => d.IdPlazo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COT_PlanTasa_COT_Plazo");

            entity.HasOne(d => d.IdTasaNavigation).WithMany(p => p.COT_PlanTasa)
                .HasForeignKey(d => d.IdTasa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COT_PlanTasa_COT_Tasa");

        }

    }
}
