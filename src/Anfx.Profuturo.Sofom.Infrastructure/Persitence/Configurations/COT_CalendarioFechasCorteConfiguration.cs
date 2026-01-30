
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_CalendarioFechasCorteConfiguration : IEntityTypeConfiguration<COT_CalendarioFechasCorte>
{
    public void Configure(EntityTypeBuilder<COT_CalendarioFechasCorte> entity)
    {
        entity.HasKey(e => e.IdCalendarioFechaCorte).HasFillFactor(90);

        entity.ToTable("COT_CalendarioFechasCorte");

        entity.Property(e => e.DiaInicio).HasColumnType("datetime");

        entity.HasOne(d => d.IdAgenciaNavigation).WithMany(p => p.COT_CalendarioFechasCorte)
            .HasForeignKey(d => d.IdAgencia)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_CalendarioFechasCorte_COT_Agencia");

        entity.HasOne(d => d.IdPeriodicidadNavigation).WithMany(p => p.COT_CalendarioFechasCorte)
            .HasForeignKey(d => d.IdPeriodicidad)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_CalendarioFechasCorte_SB_Periodicidad");

    }

}
