using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class BancoConfiguration : IEntityTypeConfiguration<Banco>
{
    public void Configure(EntityTypeBuilder<Banco> entity)
    {
        entity.HasKey(e => e.IdBanco)
            .HasName("PK__Banco__2D3F553E3F466844")
            .HasFillFactor(90);

        entity.ToTable("Banco");

        //entity.Property(e => e.ActiveAC)
        //    .IsRequired()
        //    .HasDefaultValueSql("(CONVERT([bit],(0),0))");

        entity.Property(e => e.Banco1)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("Banco");
        entity.Property(e => e.Clave_SIES)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.IdAgencia).HasDefaultValue(2);

        entity.HasOne(d => d.Agencia)
            .WithMany(p => p.Banco)
            .HasForeignKey(d => d.IdAgencia)
            .HasConstraintName("FK_Banco_COT_Agencia");

    }

}
