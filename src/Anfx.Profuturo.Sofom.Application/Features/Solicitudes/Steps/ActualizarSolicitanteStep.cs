using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Common.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class ActualizarSolicitanteStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IDatabaseService databaseServices,
    IConsecutivoService consecutivoService
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseService _databaseServices = databaseServices;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public Task<Result> CompensateAsync(CreateSolicitudContext context)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context)
    {
        if (context.IdSolicitante == 0)
            return Result.Error("NO se puede");

        var model = context.Model;

        if (model.Persona == null)
            return Result.Error("Los datos de las persona no pueden ser nulos");

        var solicitante = await _dbContext.OT_Solicitante
            .Include(i => i.OT_SolicitantePF)
            .Include(i => i.OT_SolicitantePFCuentaBancaria)
            .Include(i => i.OT_SolicitanteDireccion)
            .SingleOrDefaultAsync(r => r.IdSolicitante == context.IdSolicitante);

        if (solicitante == null)
            return Result.Error("Solicitante no encotrado");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            solicitante.Nombres_RazonSocial =
                string.IsNullOrEmpty(model.Persona?.PrimerNombre) && string.IsNullOrEmpty(model.Persona?.SegundoNombre)
                ? string.Empty
                : $"{model.Persona.PrimerNombre}-{model.Persona.SegundoNombre}";

            solicitante.RFC = model.Persona.RFC ?? "";
            solicitante.FechaNacimiento = DateTime.ParseExact(model.Persona.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            solicitante.IdPersonaJuridica = 1;
            solicitante.Email = model.Domicilio.Correo;

            var solicitantePF = solicitante.OT_SolicitantePF;
            if (solicitantePF == null)
            {
                solicitantePF = new OT_SolicitantePF { IdSolicitante = solicitante.IdSolicitante };
                solicitante.OT_SolicitantePF = solicitantePF;
            }

            solicitantePF.AniosEnElPais = 0;
            solicitantePF.ApellidoMaterno = model.Persona.ApellidoMaterno;
            solicitantePF.ApellidoPaterno = model.Persona.ApellidoPaterno;
            solicitantePF.CiudadNacimiento = model.Persona.EntidadNacimiento ?? "";
            solicitantePF.ClaveDelegacionPago = model.Persona.ClaveDelegacionPago;
            solicitantePF.ConyugePercibeIngresos = false;
            solicitantePF.CURP = model.Persona.CURP ?? "";
            solicitantePF.EsTitularTarjCr = false;
            solicitantePF.Fiel = model.Persona.FIEL ?? "";
            solicitantePF.Folio = "";
            solicitantePF.FormaMigratoria = "";
            solicitantePF.GradoEstudios = "";
            solicitantePF.GrupoPago = Convert.ToInt32(model.Persona.GrupodePago);
            solicitantePF.IdCiaTelefonica = 0;

            var IdDependenciaIMSS = _databaseServices.GetIdDelegacionIMMS(model.Persona.DependenciaIMSS);
            solicitantePF.IdDependenciaIMSS = IdDependenciaIMSS ?? 0;
            solicitantePF.IdEstadoCivil = 0;
            solicitantePF.IdGenero = model.Persona.Genero == "M" ? 1 : 2;
            solicitantePF.Nacionalidad = model.Persona.Nacionalidad;
            solicitantePF.Nextel = "";
            solicitantePF.NombreInstFinanciera = "";
            solicitantePF.NSS = Convert.ToInt64(model.Persona.NSS);
            solicitantePF.OcupacionOGiro = model.Persona.Ocupacion;
            solicitantePF.PagaInstFinanciera = false;
            solicitantePF.Pais = model.Persona.PaisNacimiento;
            solicitantePF.PaisNacimiento = model.Persona.PaisNacimiento ?? "";
            solicitantePF.RegimenMatrimonial = "";
            solicitantePF.TelefonoCasa = model.Domicilio.Telefono ?? "";
            solicitantePF.TelefonoCelular = model.Domicilio.Celular ?? "";
            solicitantePF.TieneAutoPropio = false;
            solicitantePF.CuantosAutos = 0;
            solicitantePF.Nacionalidad = model.Persona.Nacionalidad;
            solicitantePF.TipoIdentificacion = "";
            solicitantePF.AutorizoDatosPersonales = Convert.ToInt32(model.Persona.Autorizadatos);
            solicitantePF.PreguntaEstadoCuenta = model.Persona.TipoEdoCta;

            solicitantePF.CargoFederal = PLDHelper.NormalizarRespuestaPLD(model.Persona.Pregunta1PLD);
            solicitantePF.CargoFederalPariente = PLDHelper.NormalizarRespuestaPLD(model.Persona.Cargo2PLD);

            var IdCiaTelefonica = _databaseServices.GetIdCiaTelefonica(model.Domicilio.CiaTelefonica);

            solicitantePF.IdCiaTelefonica = IdCiaTelefonica;
            solicitante.OT_SolicitantePF = solicitantePF;



            var direccionPF = solicitante.OT_SolicitanteDireccion.FirstOrDefault();
            if (direccionPF == null)
            {
                var direccionPFConsecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_SolicitanteDireccion");
                if (!direccionPFConsecutivo.Success)
                    return Result.Error("No se pudo generar el consecutivo de OT_SolicitanteDireccion");

                direccionPF = new OT_SolicitanteDireccion
                {
                    IdDireccion = direccionPFConsecutivo.ConsecutivoGenerado,
                    IdSolicitante = solicitante.IdSolicitante
                };
                solicitante.OT_SolicitanteDireccion.Add(direccionPF);
            }

            var direccionIN = model.Domicilio;
            direccionPF.Calle = direccionIN.Calle;
            direccionPF.IdColonia = model.Domicilio.IdColonia;
            direccionPF.Pais = direccionIN.Pais;
            direccionPF.NumExterior = direccionIN.NumExt;
            direccionPF.NumInterior = direccionIN.NumInt;


            var cuentaBancaria = solicitante.OT_SolicitantePFCuentaBancaria.FirstOrDefault();
            if (cuentaBancaria == null)
            {
                var cuentaBancariaConsecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_SolicitantePFCuentaBancaria");
                if (!cuentaBancariaConsecutivo.Success)
                    return Result.Error("No se pudo generar el consecutivo de OT_SolicitantePFCuentaBancaria");

                cuentaBancaria = new OT_SolicitantePFCuentaBancaria
                {
                    IdCuentaBancaria = cuentaBancariaConsecutivo.ConsecutivoGenerado,
                    IdSolicitante = solicitante.IdSolicitante,
                };
                solicitante.OT_SolicitantePFCuentaBancaria.Add(cuentaBancaria);
            }

            cuentaBancaria.CuentaClabe = model.Persona.CuentaClabe;
            cuentaBancaria.Banco = model.Persona.Banco ?? "";
            cuentaBancaria.NumeroTarjeta = "";
            cuentaBancaria.CuentaBancaria = model.Persona.Cuenta;
            cuentaBancaria.IdBanco = 1;
            var idAgencia = 3;
            var banco = await _dbContext.Banco.FirstOrDefaultAsync(r=>r.Banco1 == model.Persona.Banco && (idAgencia != null && r.IdAgencia == idAgencia));
            if (banco != null)
            {
                cuentaBancaria.IdBanco = banco.IdBanco;
            }
            cuentaBancaria.IdTipoCuenta = 2;
            cuentaBancaria.SaldoPromedio = 0;
            cuentaBancaria.TipoCuenta = "";
            cuentaBancaria.FechaApertura = DateTime.Now;

            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error(ex.Message);
        }


    }
}
