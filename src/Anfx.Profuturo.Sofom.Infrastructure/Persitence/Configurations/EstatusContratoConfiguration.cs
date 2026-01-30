using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EstatusContratoConfiguration : IEntityTypeConfiguration<EstatusContrato>
{
    public void Configure(EntityTypeBuilder<EstatusContrato> entity)
    {
        entity.HasKey(e => e.IdEstatusContrato)
            .HasName("PK__EstatusC__0F5F46BF1ED998B2")
            .HasFillFactor(90);

        entity.ToTable("EstatusContrato");

        entity.Property(e => e.IdEstatusContrato).ValueGeneratedNever();
        entity.Property(e => e.EstatusContrato1)
            .HasMaxLength(30)
            .IsUnicode(false)
            .HasColumnName("EstatusContrato");

    }

}
