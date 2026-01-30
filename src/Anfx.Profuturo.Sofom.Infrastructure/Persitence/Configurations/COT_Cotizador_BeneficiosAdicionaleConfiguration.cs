using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_Cotizador_BeneficiosAdicionaleConfiguration : IEntityTypeConfiguration<COT_Cotizador_BeneficiosAdicionales>
{
    public void Configure(EntityTypeBuilder<COT_Cotizador_BeneficiosAdicionales> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("PK__COT_Coti__3214EC070DB2A4C4")
            .HasFillFactor(90);

        entity.Property(e => e.Monto).HasColumnType("decimal(13, 2)");

        entity.HasOne(d => d.IdBeneficioAdicionalNavigation).WithMany(p => p.COT_Cotizador_BeneficiosAdicionales)
            .HasForeignKey(d => d.IdBeneficioAdicional)
            .HasConstraintName("FK_COT_Cotizador_BeneficiosAdicionales_BeneficiosAdicionales");

        entity.HasOne(d => d.IdCotizadorNavigation).WithMany(p => p.COT_Cotizador_BeneficiosAdicionales)
            .HasForeignKey(d => d.IdCotizador)
            .HasConstraintName("FK_COT_Cotizador_BeneficiosAdicionales_COT_Cotizador");

    }

}
