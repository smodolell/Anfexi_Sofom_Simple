using Anfx.Profuturo.Sofom.Domain.Entities;

namespace Anfx.Profuturo.Sofom.Domain.Extensions;

public static class PlanExtensions
{
    public static decimal GetPorcentajePension(this COT_Plan plan, int tipoPension, bool tieneHijos)
    {
        if (plan == null)
            throw new ArgumentNullException(nameof(plan), "El plan no puede ser nulo.");

        return tipoPension switch
        {
            1 => tieneHijos ? plan.PH_Viudez ?? 0 : plan.Viudez ?? 0,
            2 => tieneHijos ? plan.PH_Invalidez ?? 0 : plan.Invalidez ?? 0,
            3 => tieneHijos ? plan.PH_ViudezOrfandad ?? 0 : plan.ViudezOrfandad ?? 0,
            4 => tieneHijos ? plan.PH_Vejez ?? 0 : plan.Vejez ?? 0,
            5 => tieneHijos ? plan.PH_CesantiaEdadAvanzada ?? 0 : plan.CesantíaEdadAvanzada ?? 0,
            6 => tieneHijos ? plan.PH_RetiroAnticipado ?? 0 : plan.RetiroAnticipado ?? 0,
            7 => tieneHijos ? plan.PH_Tipo1 ?? 0 : plan.Tipo1 ?? 0,
            8 => tieneHijos ? plan.PH_Tipo2 ?? 0 : plan.Tipo2 ?? 0,
            9 => tieneHijos ? plan.PH_Tipo3 ?? 0 : plan.Tipo3 ?? 0,
            10 => tieneHijos ? plan.PH_Tipo4 ?? 0 : plan.Tipo4 ?? 0,
            _ => 0
        };
    }

    // Método de extensión adicional que incluye la búsqueda por ID
    public static decimal GetPorcentajePension(this IQueryable<COT_Plan> query, int idPlan, int tipoPension, bool tieneHijos)
    {
        var plan = query.FirstOrDefault(p => p.IdPlan == idPlan);
        if (plan == null)
            throw new InvalidOperationException($"Plan con ID {idPlan} no encontrado.");

        return plan.GetPorcentajePension(tipoPension, tieneHijos);
    }

    public static bool EstaVigente(this COT_Plan plan)
    {
        if (plan == null ||
            !plan.FechaVigenciaIni.HasValue ||
            !plan.FechaVigenciaFin.HasValue)
            return false;

        var fechaActual = DateTime.Now.Date;
        return fechaActual >= plan.FechaVigenciaIni.Value.Date &&
               fechaActual <= plan.FechaVigenciaFin.Value.Date;
    }

}

