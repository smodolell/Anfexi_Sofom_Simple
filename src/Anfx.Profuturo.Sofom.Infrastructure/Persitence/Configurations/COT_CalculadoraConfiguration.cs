using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_CalculadoraConfiguration : IEntityTypeConfiguration<COT_Calculadora>
{
    public void Configure(EntityTypeBuilder<COT_Calculadora> entity)
    {
        entity.HasKey(e => e.IdCalculadora);
        entity.ToTable("COT_Calculadora");
 
    }

}
