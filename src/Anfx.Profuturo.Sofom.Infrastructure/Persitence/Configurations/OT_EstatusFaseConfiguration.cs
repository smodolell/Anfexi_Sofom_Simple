using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations
{
    public partial class OT_EstatusFaseConfiguration : IEntityTypeConfiguration<OT_EstatusFase>
    {
        public void Configure(EntityTypeBuilder<OT_EstatusFase> entity)
        {
            entity.HasKey(e => e.IdEstatusFase);

            entity.ToTable("OT_EstatusFase");

            entity.Property(e => e.IdEstatusFase).ValueGeneratedNever();
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);


        }

    }
}
