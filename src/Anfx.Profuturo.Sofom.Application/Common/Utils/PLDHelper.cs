namespace Anfx.Profuturo.Sofom.Application.Common.Utils;

public static class PLDHelper
{
    public static string NormalizarRespuestaPLD(string? respuesta)
    {
        if (string.IsNullOrWhiteSpace(respuesta))
            return "";

        var respuestaNormalizada = respuesta.Trim().ToUpperInvariant();

        // Lista de valores que se consideran negativos
        var valoresNegativos = new HashSet<string>
        {
            "FALSE", "NO", "0", "FALSO", "NEGATIVO", "N", "F"
        };

        return valoresNegativos.Contains(respuestaNormalizada)
            ? ""
            : respuesta;
    }
}