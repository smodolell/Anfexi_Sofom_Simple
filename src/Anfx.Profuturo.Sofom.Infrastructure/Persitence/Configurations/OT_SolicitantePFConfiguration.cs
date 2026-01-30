using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class OT_SolicitantePFConfiguration : IEntityTypeConfiguration<OT_SolicitantePF>
{
    public void Configure(EntityTypeBuilder<OT_SolicitantePF> entity)
    {
        entity.HasKey(e => e.IdSolicitante)
            .HasName("PK__OT_Solic__B6EB12004159993F")
            .HasFillFactor(90);

        entity.ToTable("OT_SolicitantePF");

        entity.HasIndex(e => e.CURP, "Mejora_DBA_120302").HasFillFactor(90);

        entity.HasIndex(e => e.NSS, "dba_index_148189").HasFillFactor(90);

        entity.Property(e => e.IdSolicitante).ValueGeneratedNever();
        entity.Property(e => e.ApellidoMaterno).HasMaxLength(60);
        entity.Property(e => e.ApellidoPaterno).HasMaxLength(60);
        entity.Property(e => e.CURP)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.CargoFederal)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.CargoFederalPariente)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.CiudadNacimiento)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.ClaveDelegacionPago)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.FechaPrivacidadNomina).HasColumnType("datetime");
        entity.Property(e => e.Fiel)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.Folio)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.FormaMigratoria)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.GradoEstudios)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.IdEntidadFederativa)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.Institucion)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.Nacionalidad)
            .HasMaxLength(60)
            .IsUnicode(false);
        entity.Property(e => e.Nextel)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NombreInstFinanciera)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NumeroSerieElectronica)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.OcupacionOGiro)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.Pais)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.PaisNacimiento)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.PreguntaEstadoCuenta)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.RegimenMatrimonial)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.TelefonoCasa)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.TelefonoCelular)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.TipoIdentificacion)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasOne(d => d.IdCiaTelefonicaNavigation).WithMany(p => p.OT_SolicitantePF)
            .HasForeignKey(d => d.IdCiaTelefonica)
            .HasConstraintName("SolPFCiaTelefonica");

        entity.HasOne(d => d.IdSolicitanteNavigation).WithOne(p => p.OT_SolicitantePF)
            .HasForeignKey<OT_SolicitantePF>(d => d.IdSolicitante)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OT_SolicitantePF_OT_Solicitante");
     
    }

}
