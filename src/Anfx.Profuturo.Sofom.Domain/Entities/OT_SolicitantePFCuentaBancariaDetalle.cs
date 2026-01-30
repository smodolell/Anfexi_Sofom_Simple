using System;
using System.Collections.Generic;

namespace Anfx.Profuturo.Sofom.Domain.Entities;

public partial class OT_SolicitantePFCuentaBancariaDetalle
{
    public int IdCuentaBancariaDetalle { get; set; }

    public int IdSolicitante { get; set; }

    public bool RecursoTerceraPersona { get; set; }

    public string Nombre { get; set; }

    public string ApellidoPaterno { get; set; }

    public string ApellidoMaterno { get; set; }

    public string TelefonoCasa { get; set; }

    public string TelefonoCelular { get; set; }

    public string Email { get; set; }

    public bool CargoPublico { get; set; }

    public string CargoPublicoDescripcion { get; set; }

    public bool ParentescoCargoPublico { get; set; }

    public string ParentescoCargoPublicoDescripcion { get; set; }

    public OT_Solicitante IdSolicitanteNavigation { get; set; }
}