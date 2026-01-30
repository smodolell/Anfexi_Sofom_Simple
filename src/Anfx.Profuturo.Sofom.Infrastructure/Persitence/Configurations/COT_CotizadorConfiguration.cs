using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anfx.Profuturo.Sofom.Infrastructure.Persitence.Configurations;

public partial class COT_CotizadorConfiguration : IEntityTypeConfiguration<COT_Cotizador>
{
    public void Configure(EntityTypeBuilder<COT_Cotizador> entity)
    {
        entity.HasKey(e => e.IdCotizador)
            .HasName("PK__COT_Coti__B3F722405807F46D")
            .HasFillFactor(90);

        entity.ToTable("COT_Cotizador");

        entity.HasIndex(e => e.PrestamoVoz, "IX_COT_Cotizador_220225");

        entity.HasIndex(e => new { e.IdAgencia, e.PrestamoVoz }, "IX_COT_Cotizador_220225_2");

        entity.HasIndex(e => e.NombreCliente, "MejoraDBA02_index_1976975").HasFillFactor(90);


        entity.HasIndex(e => e.CURP, "Mejora_DBA_1039435").HasFillFactor(90);

        entity.HasIndex(e => e.Folio, "Mejora_IDX_134476");

        entity.HasIndex(e => e.Folio, "Mejora_IDX_27265");

        entity.HasIndex(e => e.Folio, "Mejora_IDX_30195");

        entity.HasIndex(e => e.Folio, "Mejora_IDX_51");

        entity.HasIndex(e => e.RFC, "_dta_index_COT_Cotizador_9_1444916219__K4_1040").HasFillFactor(90);

        entity.HasIndex(e => e.Folio, "dba3_index_481");

        entity.HasIndex(e => e.Folio, "dba3_index_5");

        entity.HasIndex(e => new { e.IdAgencia, e.IdPlan }, "dba3_index_889");

        entity.HasIndex(e => e.IdPlan, "dba_index_148227");

        entity.HasIndex(e => e.IdPlan, "dba_index_148268");

        entity.HasIndex(e => new { e.IdAgencia, e.Folio }, "dba_index_149332");

        entity.Property(e => e.IdCotizador).ValueGeneratedNever();
        entity.Property(e => e.ApellidoMaterno)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.ApellidoPaterno)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasDefaultValue("");
        entity.Property(e => e.CAT).HasColumnType("decimal(12, 6)");
        entity.Property(e => e.CURP)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.CalculoCAT).HasColumnType("decimal(12, 6)");
        entity.Property(e => e.CapacidadPagoInforme).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ClaveRastreoBancaria)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.DescuentosFijosMensual).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.FechaIngresoEmpresa).HasColumnType("datetime");
        entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
        entity.Property(e => e.FechaSimulacion).HasColumnType("datetime");
        entity.Property(e => e.Folio)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.GrupoPago)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.MontoPension).HasColumnType("decimal(12, 4)");
        entity.Property(e => e.MontoPensionOrfandad).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.MontoPrestamo).HasColumnType("decimal(12, 4)");
        entity.Property(e => e.MontoQDeceaPagar).HasColumnType("decimal(12, 4)");
        entity.Property(e => e.MontoSolicitar).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.NSS)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.NombreCliente)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.NumeroPagosFijos).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PagoFijoTotalAproximado).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PagoFijoTotalAproximadoMensual).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.PorcentajeSeguro).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.PrestamoVoz).HasDefaultValue(false);
        entity.Property(e => e.RFC)
            .HasMaxLength(13)
            .IsUnicode(false);
        entity.Property(e => e.Seguro).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.SuapSipre)
            .HasMaxLength(13)
            .IsUnicode(false);
        entity.Property(e => e.SueldoDisponible).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.SueldoNetoMensual).HasColumnType("decimal(13, 2)");
        entity.Property(e => e.Tasa).HasColumnType("decimal(9, 7)");
        entity.Property(e => e.ValidaClaveRastreoBancaria).HasDefaultValue(false);

        entity.HasOne(d => d.IdAgenciaNavigation).WithMany(p => p.COT_Cotizador)
            .HasForeignKey(d => d.IdAgencia)
            .HasConstraintName("FK_COT_CotizadorNuevo_COT_Agencia");

        entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.COT_Cotizador)
            .HasForeignKey(d => d.IdPlan)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_COT_CotizadorNuevo_COT_Plan");

    }

}
