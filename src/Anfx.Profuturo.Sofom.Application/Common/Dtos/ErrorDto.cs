namespace Anfx.Profuturo.Sofom.Application.Common.Dtos;

public class ErrorDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;

    public ErrorDto(string codigo, string mensaje)
    {
        Codigo = codigo;
        Mensaje = mensaje;
    }

    public ErrorDto()
    {
    }
}
