using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_FaseConfiguration : IEntityTypeConfiguration<OT_Fase>
{
    public void Configure(EntityTypeBuilder<OT_Fase> entity)
    {
        entity.HasKey(e => e.IdFase);

        entity.ToTable("OT_Fase");

        entity.Property(e => e.IdFase).ValueGeneratedNever();
        entity.Property(e => e.Accion)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.ClaveUnica)
            .IsRequired()
            .HasMaxLength(5)
            .IsUnicode(false);
        entity.Property(e => e.Controlador)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Icono)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.MailAdicional)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.NombreFase)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Orden).HasColumnType("decimal(12, 2)");
        entity.Property(e => e.ProcedimientoValidacion)
            .HasMaxLength(200)
            .IsUnicode(false);

    }

}
