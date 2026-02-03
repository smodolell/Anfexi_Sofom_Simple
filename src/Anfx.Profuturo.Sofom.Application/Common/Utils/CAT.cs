using Microsoft.VisualBasic;

namespace Anfx.Profuturo.Sofom.Application.Common.Utils;


public class CAT
{
    public Decimal MontoPrestamo { get; set; }
    public Decimal Comision { get; set; }
    public int NoPagos { get; set; }
    public double PagosEnElAnio { get; set; }

    protected Decimal[] TablaAmortiza { get; set; }

    private double[] PagoFlujoTotal { get; set; }

    public CAT()
    {

    }

    /// <summary>Calculo del CAT
    /// <para>Recibe 5 Parametros:</para>
    /// <para>_MontoPrestamo: Monto Capital a Financiar.</para>
    /// <para>_Comision: Monto de comision de apertura</para>
    /// <para>_NoPagos: Total de Pagos del Crédito</para>
    /// <para>_TablaAmortiza: Tablas de pagos Iguales o Diferentes, Montos Totales.</para>
    /// <para>_Periodicidad: 1 -> SEMANAL ,  2 -> QUINCENAL , 3 -> MENSUAL</para>
    /// </summary> 
    public CAT(Decimal _MontoPrestamo, Decimal _Comision, int _NoPagos, Decimal[] _TablaAmortiza, int _Periodicidad)
    {
        MontoPrestamo = _MontoPrestamo;
        Comision = _Comision;
        NoPagos = _NoPagos;
        TablaAmortiza = _TablaAmortiza;

        switch (_Periodicidad)
        {
            case 1: // Semanal
                PagosEnElAnio = 52;
                break;
            case 2: // Quincenal
                PagosEnElAnio = 24;
                break;
            case 3: // Mensual
                PagosEnElAnio = 12;
                break;
            case 6: // Quincenal
                PagosEnElAnio = 24;
                break;
        }
    }

    private bool GetPagoFlujoTotal()
    {
        PagoFlujoTotal = new double[NoPagos + 1];
        PagoFlujoTotal[0] = (double)((-1 * MontoPrestamo) + Comision);
        var i = 1;
        try
        {
            foreach (var row in TablaAmortiza.Take(NoPagos))
            {
                PagoFlujoTotal[i] = Convert.ToDouble(row);
                i++;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public double CalculaCAT()
    {
        if (GetPagoFlujoTotal())
        {
            var temp = PagoFlujoTotal;

            try
            {
                var irr = Financial.IRR(ref temp, 0);
                var cat = +(Math.Pow((1.00 + irr), PagosEnElAnio)) - 1;
                return cat;
            }
            catch (Exception)
            {
                return -1.00;
            }

        }
        return -1.00;
    }

}
