
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.DTOs;
using Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion.Strategies.Calculadora;

public abstract class CalculadoraStrategyBase : ICalculadoraStrategy
{
    private readonly IApplicationDbContext _dbContext;
    protected readonly ICalculoHelper _calculoHelper;

    protected CalculadoraStrategyBase(
       IApplicationDbContext dbContext,
        ICalculoHelper calculoHelper)
    {
        _dbContext = dbContext;
        _calculoHelper = calculoHelper;
    }

    public abstract bool AplicaPara(CalculoContextDto context);

    public abstract Task<CotizadorOpcionDto> GetOpcion(CalculoContextDto context);

    protected virtual async Task LoadData(CalculoContextDto context)
    {
        // Cargar plan
        var plan = await _dbContext.COT_Plan
            .Include(i => i.COT_PlanComision)
                .ThenInclude(t => t.IdComisionNavigation)
            .Include(i => i.COT_PlanTasa)
                .ThenInclude(t => t.IdTasaNavigation)
            .Include(i => i.COT_PlanPlazo)
                .ThenInclude(t => t.IdPlazoNavigation)
            .SingleOrDefaultAsync(r => r.IdPlan == context.IdPlan);

        if (plan == null)
            throw new InvalidOperationException($"No se encontró el plan con ID {context.IdPlan}");

        // Cargar comisión
        var comision = plan.COT_PlanComision
            .FirstOrDefault(r => r.IdPlazo == context.IdPlazo)?.IdComisionNavigation;

        // Cargar tasa
        var tasa = plan.COT_PlanTasa
            .FirstOrDefault(r => r.IdPlazo == context.IdPlazo)?.IdTasaNavigation;

        // Cargar plazo
        var plazo = plan.COT_PlanPlazo
            .FirstOrDefault(r => r.IdPlazo == context.IdPlazo)?.IdPlazoNavigation;

        // Cargar periodicidad
        var periodicidad = await _dbContext.SB_Periodicidad.SingleOrDefaultAsync(r => r.IdPeriodicidad == context.IdPeriodicidad);

        if (plazo == null)
            throw new InvalidOperationException($"No se encontró plazo con ID {context.IdPlazo} para el plan {context.IdPlan}");

        if (tasa == null)
            throw new InvalidOperationException($"No se encontró tasa para el plazo {context.IdPlazo}");

        if (periodicidad == null)
            throw new InvalidOperationException($"No se encontró periodicidad con ID {context.IdPeriodicidad}");

        // Asignar al contexto
        context.Plan = plan;
        context.Plazo = plazo;
        context.Tasa = tasa;
        context.Comision = comision;
        context.Periodicidad = periodicidad;


        context.ValorTasa = tasa.Valor ?? 0;

        if (context.EsReestructura)
        {
            var contratos = context.ContratosReestructura.Split('_').ToList();

            var contrato = _dbContext.Contrato
                   .Where(r => contratos.Contains(r.Contrato1))
                   .OrderByDescending(r => r.FecActivacion)
                   .FirstOrDefault();

            if (contrato == null) throw new Exception("EL ULTIMO CONTRATO ACTIVO NO EXISTE");

            context.ValorTasa = Convert.ToDecimal(contrato.Tasa);
        }



        // Asignar valores calculados
        context.InteresAnualIVA = _calculoHelper.CalcularTasaConIVA(context.ValorTasa); ;
        context.PorcentajeSeguro = comision?.Porcentaje ?? 0;

        context.PorcentajePencion = plan.GetPorcentajePension(
            context.IdPension,
            context.TienePensionHijos);


    }

    protected CotizadorOpcionDto ConstruirOpcionBase(CalculoContextDto context, decimal montoPrestamo, decimal descuentoMensual)
    {
        return new CotizadorOpcionDto
        {
            idPlan = context.IdPlan,
            Plazo = context.Plazo?.Plazo ?? 0,
            MontoPrestamo = montoPrestamo,
            Seguro = 0, // Se calculará después
            PorcentajeSeguro = context.PorcentajeSeguro,
            DescuentoMensual = descuentoMensual,
            Tasa = context.ValorTasa,
            FolioConfirmacion = context.FolioConfirmacion ?? "0",
            EsReestructura = context.EsReestructura ? 1 : 0,
            ContratosReestructura = context.ContratosReestructura ?? ""
        };
    }
}