
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

//public partial class COT_PlanComisionConfiguration : IEntityTypeConfiguration<COT_PlanComision>
//{
//    public void Configure(EntityTypeBuilder<COT_PlanComision> entity)
//    {
//        entity.HasKey(e => new { e.IdComision, e.IdPlazo });

//        entity.ToTable("COT_ComisionPlazo");

//        entity.HasOne(d => d.IdComisionNavigation).WithMany(p => p.COT_PlanComision)
//            .HasForeignKey(d => d.IdComision)
//            .OnDelete(DeleteBehavior.ClientSetNull)
//            .HasConstraintName("FK_COT_ComisionPlazo_COT_Comision");

//        entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.COT_PlanComision)
//            .HasForeignKey(d => d.IdPlazo)
//            .OnDelete(DeleteBehavior.ClientSetNull)
//            .HasConstraintName("FK_COT_ComisionPlazo_COT_Plazo");

//    }

//}
