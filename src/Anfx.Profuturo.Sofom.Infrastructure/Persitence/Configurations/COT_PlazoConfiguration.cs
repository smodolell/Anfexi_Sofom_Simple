using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class COT_PlazoConfiguration : IEntityTypeConfiguration<COT_Plazo>
    {
        public void Configure(EntityTypeBuilder<COT_Plazo> entity)
        {
            entity.HasKey(e => e.IdPlazo).HasName("PK_Plazo");

            entity.ToTable("COT_Plazo");

            entity.Property(e => e.IdPlazo).ValueGeneratedNever();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<COT_Plazo> entity);
    }
}
