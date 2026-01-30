using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class COT_TasaConfiguration : IEntityTypeConfiguration<COT_Tasa>
    {
        public void Configure(EntityTypeBuilder<COT_Tasa> entity)
        {
            entity.HasKey(e => e.IdTasa)
                .HasName("PK__COT_Tasa__9FCAD1CF742F31CF")
                .HasFillFactor(90);

            entity.ToTable("COT_Tasa");

            entity.Property(e => e.IdTasa).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PuntajeMaximo).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PuntajeMinimo).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Valor).HasColumnType("decimal(9, 7)");

            entity.HasOne(d => d.IdPlazoNavigation).WithMany(p => p.COT_Tasa)
                .HasForeignKey(d => d.IdPlazo)
                .HasConstraintName("FK_COT_Tasa_COT_Plazo");
        }

    }
}
