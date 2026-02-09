using Anfx.Profuturo.Sofom.Application.Common.Saga;
using Anfx.Profuturo.Sofom.Application.Common.Utils;
using Anfx.Profuturo.Sofom.Application.Features.Solicitudes.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Anfx.Profuturo.Sofom.Application.Features.Solicitudes.Steps;

public class CrearSolicitanteStep(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IDatabaseService databaseService,
    IConsecutivoService consecutivoService
) : ISagaStep<CreateSolicitudContext>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IDatabaseService _databaseService = databaseService;
    private readonly IConsecutivoService _consecutivoService = consecutivoService;

    public Task<Result> CompensateAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }

    public async Task<Result> ExecuteAsync(CreateSolicitudContext context, CancellationToken cancellationToken = default)
    {
        var model = context.Model;
        try
        {


            // Obtener la solicitud actual del contexto
            var solicitud = await _dbContext.OT_Solicitud
                .SingleOrDefaultAsync(r => r.IdSolicitud == context.IdSolicitud);

            if (solicitud == null)
                return Result.NotFound("Solicitud no encontrada");


            var cotizador = await _dbContext.COT_Cotizador
                .SingleOrDefaultAsync(r => r.Folio == context.Folio);

            if (cotizador == null)
                return Result.NotFound("Cotizador no encontrado");

            await _unitOfWork.BeginTransactionAsync();


            var solicitante = await _dbContext.OT_Solicitante
                .Include(i => i.OT_SolicitantePF)
                .Include(i => i.OT_SolicitanteDireccion)
                .SingleOrDefaultAsync(r => r.IdSolicitante == (solicitud.IdSolicitante ?? 0));
            var existeSolicitante = solicitante != null;
            if (solicitante == null)
            {
                var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_Solicitante");
                if (!consecutivo.Success) throw new Exception("No se pudo generar el consecutivo de OT_Solictante");
                solicitante = new OT_Solicitante
                {
                    IdSolicitante = consecutivo.ConsecutivoGenerado,
                };
                solicitud.IdSolicitanteNavigation = solicitante;
            }


            var solicitantePF = solicitante.OT_SolicitantePF;

            if (solicitantePF == null)
            {
                solicitantePF = new OT_SolicitantePF
                {
                    IdSolicitante = solicitante.IdSolicitante,
                };
                solicitante.OT_SolicitantePF = solicitantePF;
            }



            MapSolicitante(model, solicitante);
            MapSolicitantePF(model, solicitante);
            await MapCuentaBancaria(model, solicitante);
            await MapDireccion(model, solicitante);


            if (!existeSolicitante)
            {
                _dbContext.OT_Solicitante.Update(solicitante);
                _dbContext.OT_Solicitud.Update(solicitud);
            }



            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Error($"Error al crear solicitante: {ex.Message}");
        }
    }


    private void MapSolicitante(SolicitudModel model, OT_Solicitante solicitante)
    {
        solicitante.Nombres_RazonSocial = $"{model.Persona.PrimerNombre}-{model.Persona.SegundoNombre}";
        solicitante.RFC = model.Persona.RFC ?? "";
        solicitante.FechaNacimiento = DateTime.ParseExact(model.Persona.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        solicitante.IdPersonaJuridica = 1;
        solicitante.Email = model.Domicilio.Correo;
    }
    private void MapSolicitantePF(SolicitudModel model, OT_Solicitante solicitante)
    {

        var solicitantePF = solicitante.OT_SolicitantePF;
        if (solicitantePF == null)
        {
            if (solicitantePF == null)
            {
                solicitantePF = new OT_SolicitantePF
                {
                    IdSolicitante = solicitante.IdSolicitante,
                };
                solicitante.OT_SolicitantePF = solicitantePF;
            }
        }
        solicitantePF.AniosEnElPais = 0;
        solicitantePF.ApellidoMaterno = model.Persona.ApellidoMaterno;
        solicitantePF.ApellidoPaterno = model.Persona.ApellidoPaterno;
        solicitantePF.CiudadNacimiento = model.Persona.EntidadNacimiento ?? "";
        solicitantePF.ConyugePercibeIngresos = false;
        solicitantePF.CURP = model.Persona.CURP ?? "";
        solicitantePF.EsTitularTarjCr = false;
        solicitantePF.Fiel = model.Persona.FIEL ?? "";
        solicitantePF.Folio = "";
        solicitantePF.FormaMigratoria = "";
        solicitantePF.GradoEstudios = "";
        solicitantePF.GrupoPago = Convert.ToInt32(model.Persona.GrupodePago);
        solicitantePF.Nacionalidad = model.Persona.Nacionalidad;
        solicitantePF.Nextel = "";
        solicitantePF.NombreInstFinanciera = "";
        solicitantePF.NSS = Convert.ToInt64(model.Persona.NSS);
        solicitantePF.NumeroSerieElectronica = model.Persona.FIEL ?? "";
        solicitantePF.OcupacionOGiro = model.Persona.Ocupacion;
        solicitantePF.PagaInstFinanciera = false;
        solicitantePF.Pais = model.Persona.PaisNacimiento;
        solicitantePF.PaisNacimiento = model.Persona.PaisNacimiento ?? "";
        solicitantePF.RegimenMatrimonial = "";
        solicitantePF.TelefonoCasa = model.Domicilio.Telefono ?? "";
        solicitantePF.TelefonoCelular = model.Domicilio.Celular ?? "";
        solicitantePF.TieneAutoPropio = false;
        solicitantePF.CuantosAutos = 0;
        solicitantePF.TipoIdentificacion = "";
        solicitantePF.AutorizoDatosPersonales = Convert.ToInt32(model.Persona.Autorizadatos);
        solicitantePF.PreguntaEstadoCuenta = model.Persona.TipoEdoCta;

        // Lógica para delegación IMSS (combinación de ambas)
        string claveDependencia;
        //if (!esModificacion || string.IsNullOrEmpty(model.Persona.ClaveDelegacionPago))
        //{
        //    // Lógica del método CrearSolicitante
        //    var IdDependenciaIMSS = await _unitOfWork.QueryAsync<int?>(
        //        "SELECT CLAVE FROM dbo.OT_DelegacionIMMS WHERE Clave = @clave",
        //        new { clave = model.Persona.ClaveDelegacionPago });
        //    claveDependencia = IdDependenciaIMSS.FirstOrDefault().HasValue ? model.Persona.ClaveDelegacionPago : "01";
        //}
        //else
        //{
        //    // Lógica del método ModificarSolicitante
        //    var IdDependenciaIMSS = await _unitOfWork.QueryAsync<int?>(
        //        "SELECT CLAVE FROM dbo.OT_DelegacionIMMS WHERE Delegacion = @delegacion",
        //        new { delegacion = model.Persona.DependenciaIMSS });
        //    claveDependencia = IdDependenciaIMSS.FirstOrDefault()?.ToString() ?? "01";
        //}

        //solicitantePF.ClaveDelegacionPago = claveDependencia;
        //solicitantePF.IdDependenciaIMSS = Convert.ToInt32(claveDependencia);

        // Lógica para PLD (común)
        solicitantePF.CargoFederal = PLDHelper.NormalizarRespuestaPLD(model.Persona.Pregunta1PLD);
        solicitantePF.CargoFederalPariente = PLDHelper.NormalizarRespuestaPLD(model.Persona.Cargo2PLD);

        var IdCiaTelefonica = _databaseService.GetIdCiaTelefonica(model.Domicilio.CiaTelefonica);



        solicitantePF.IdCiaTelefonica = IdCiaTelefonica;
        solicitantePF.IdEstadoCivil = 0;
        solicitantePF.IdGenero = model.Persona.Genero == "M" ? 1 : 2;
        solicitantePF.IdEntidadFederativa = model.Persona.EntidadNacimiento ?? "";

    }


    private async Task MapDireccion(SolicitudModel model, OT_Solicitante solicitante)
    {
        var direccion = solicitante.OT_SolicitanteDireccion.FirstOrDefault();
        if (direccion == null)
        {
            var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_SolicitanteDireccion");
            if (!consecutivo.Success)
                throw new Exception("No se pudo generar el consecutivo de OT_SolicitanteDireccion");

            direccion = new OT_SolicitanteDireccion
            {
                IdDireccion = consecutivo.ConsecutivoGenerado,
                IdSolicitante = solicitante.IdSolicitante,
            };
            solicitante.OT_SolicitanteDireccion.Add(direccion);
        }

        direccion.Calle = model.Domicilio.Calle;
        direccion.IdColonia = model.Domicilio.IdColonia;
        direccion.Pais = model.Domicilio.Pais;
        direccion.NumExterior = model.Domicilio.NumExt;
        direccion.NumInterior = model.Domicilio.NumInt;


    }

    private async Task MapCuentaBancaria(SolicitudModel model, OT_Solicitante solicitante)
    {
        var cuenta = solicitante.OT_SolicitantePFCuentaBancaria.FirstOrDefault();

        if (cuenta == null)
        {
            var consecutivo = await _consecutivoService.ObtenerSiguienteConsecutivoAsync("OT_SolicitantePFCuentaBancaria");
            if (!consecutivo.Success)
                throw new Exception("No se pudo generar el consecutivo de OT_SolicitantePFCuentaBancaria");

            cuenta = new OT_SolicitantePFCuentaBancaria
            {
                IdCuentaBancaria = consecutivo.ConsecutivoGenerado,
                IdSolicitante = solicitante.IdSolicitante
            };
        }

        cuenta.CuentaClabe = model.Persona.CuentaClabe;
        cuenta.Banco = model.Persona.Banco ?? "";
        cuenta.NumeroTarjeta = "";
        cuenta.CuentaBancaria = model.Persona.Cuenta ?? "";
        cuenta.NroSucursal = Convert.ToInt32(model.Persona.NoSucursalBancaria ?? "0");
        cuenta.IdTipoCuenta = 2;
        cuenta.SaldoPromedio = 0;
        cuenta.TipoCuenta = "";
        cuenta.FechaApertura = DateTime.Now;

        // Lógica combinada para banco
        string? nombreBanco = model.Persona.Banco?.Trim();
        int idBancoAsignado;
        int idDefaultBanco = 1;

        if (string.IsNullOrWhiteSpace(nombreBanco))
        {
            idBancoAsignado = idDefaultBanco;
        }
        else
        {
            // Primero intentar encontrar banco existente (método CrearSolicitante)
            var bancoExistente = await _dbContext.Banco.FirstOrDefaultAsync(r => r.Banco1 == nombreBanco && r.IdAgencia == 3);

            if (bancoExistente != null)
            {
                idBancoAsignado = bancoExistente.IdBanco;
            }
            else
            {

                if (bancoExistente != null)
                {
                    idBancoAsignado = bancoExistente.IdBanco;
                }
                else
                {
                    // Crear nuevo banco (método CrearSolicitante)
                    try
                    {
                        var nuevoBanco = new Banco
                        {
                            Banco1 = nombreBanco,
                            IdAgencia = 3,
                        };
                        await _dbContext.Banco.AddAsync(nuevoBanco);
                        idBancoAsignado = nuevoBanco.IdBanco;
                    }
                    catch
                    {
                        idBancoAsignado = idDefaultBanco;
                    }
                }
            }
        }

        cuenta.IdBanco = idBancoAsignado;

    }
}
