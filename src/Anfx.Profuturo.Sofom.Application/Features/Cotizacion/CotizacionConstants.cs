namespace Anfx.Profuturo.Sofom.Application.Features.Cotizacion;

public class CotizacionConstants
{
    // Agencias
    public const int ID_AGENCIA_IMSS = 2;
    public const int ID_AGENCIA_PENSIONES = 3;

    // Periodicidades
    public const int ID_PERIODICIDAD_MENSUAL = 3;

    // Tipos de Seguro
    public const int ID_TIPO_SEGURO_PIRAMIDAL = 1;
    public const int ID_TIPO_SEGURO_GASTOS_FUNERARIOS = 2;
    public const int ID_TIPO_SEGURO_SIN_SEGURO = 3;

    // Tipos de Cotización
    public static class TipoCotizacion
    {
        public const int CapacidadDescuento = 1;
        //public const int MontoPensionSueldo = 2;
        public const int MontoPrestamo = 3;
        //public const int MontoDepositar = 4;

        public static bool EsCapacidadDescuento(int tipo) => tipo == CapacidadDescuento;
        //public static bool EsMontoPension(int tipo) => tipo == MontoPensionSueldo;
        public static bool EsMontoPrestamo(int tipo) => tipo == MontoPrestamo;
        //public static bool EsMontoDepositar(int tipo) => tipo == MontoDepositar;
    }

    // Tipos de Pensión
    public static class TipoPension
    {
        public const int Viudez = 1;
        public const int Invalidez = 2;
        public const int ViudezOrfandad = 3;
        public const int Vejez = 4;
        public const int CesantiaEdadAvanzada = 5;
        public const int RetiroAnticipado = 6;
        public const int Tipo1 = 7;
        public const int Tipo2 = 8;
        public const int Tipo3 = 9;
        public const int Tipo4 = 10;

        // Métodos helper para identificar tipos
        public static bool EsViudez(int tipo) => tipo == Viudez;
        public static bool EsInvalidez(int tipo) => tipo == Invalidez;
        public static bool EsViudezOrfandad(int tipo) => tipo == ViudezOrfandad;
        public static bool EsPensionHijos(int tipo) => tipo >= 1 && tipo <= 10; // Todos los tipos aplican

        public static decimal ObtenerPorcentajeDefault(int tipo)
        {
            return tipo switch
            {
                Viudez => 0.30m,      // 30%
                Invalidez => 0.35m,   // 35%
                ViudezOrfandad => 0.40m, // 40%
                Vejez => 0.45m,       // 45%
                _ => 0.30m            // Default
            };
        }
    }

    // Constantes Matemáticas/Financieras
    public const decimal IVA_PORCENTAJE = 16.00m;
    public const decimal IVA_FACTOR = 1.16m;
    public const int DIAS_POR_MES = 30;
    public const int MESES_POR_ANIO = 12;
    public const int DIAS_POR_ANIO = 360;

    // Constantes de Validación
    public const decimal MONTO_ORFANDAD_UMBRAL = 1.0m;
    public const decimal PORCENTAJE_MINIMO_VALIDO = 0.01m;
    public const decimal MONTO_MINIMO_VALIDO = 0.01m;

    // Configuración de Cálculos
    public const int PRECISION_CALCULOS = 3;
    public const int PRECISION_MONEDA = 2;
    public const decimal PORCENTAJE_CAPACIDAD_PAGO = 30.00m; // 30%
    public const decimal PORCENTAJE_PENSION_ALIMENTACION = 30.00m; // 30%

    // Mensajes de Error (para centralizar)
    public static class MensajesError
    {
        public const string CONTRATO_NO_ENCONTRADO = "CONTRATO DE RESTRUCTURA NO ENCONTRADO";
        public const string ULTIMO_CONTRATO_NO_EXISTE = "EL ULTIMO CONTRATO ACTIVO NO EXISTE";
        public const string MONTO_PRESTAMO_INVALIDO = "Monto del préstamo debe ser positivo";
    }

    // Factores de Cálculo
    public const decimal FACTOR_SEGURO_PIRAMIDAL = 1000.00m;

    // Otros valores constantes
    public const int USUARIO_DEFAULT_ASESOR = 435;
    public const int USUARIO_DEFAULT_ASESOR_REESTRUCTURA = 344;
    public const int PERSONA_JURIDICA_DEFAULT = 1;
    public const int ESTATUS_SOLICITUD_INICIAL = 1;
}

