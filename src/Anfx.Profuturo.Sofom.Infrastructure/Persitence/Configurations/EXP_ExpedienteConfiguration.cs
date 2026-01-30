using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class EXP_ExpedienteConfiguration : IEntityTypeConfiguration<EXP_Expediente>
{
    public void Configure(EntityTypeBuilder<EXP_Expediente> entity)
    {
        entity.HasKey(e => e.IdExpediente).HasFillFactor(90);

        entity.ToTable("EXP_Expediente");

        entity.HasIndex(e => new { e.IdDuenioExpediente, e.IdExpediente }, "Mejora_DBA_195");

        entity.HasIndex(e => new { e.IdQuePersona, e.IdDuenioExpediente }, "index_EXP_ExpedienteDue");

        entity.Property(e => e.IdExpediente).ValueGeneratedNever();

    }

}
