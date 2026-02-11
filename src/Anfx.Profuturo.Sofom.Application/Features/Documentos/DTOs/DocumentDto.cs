namespace Anfx.Profuturo.Sofom.Application.Features.Documentos.DTOs;

public class DocumentDto
{
    public string EstadoSolicitud { get; set; } = string.Empty;
    public string EstadoFase { get; set; } = string.Empty;
    public string FechaOperacion { get; set; } = string.Empty;
    public string Comentario { get; set; }=string.Empty;
    public List<FaseItem> HistoriaFases { get; set; } = new List<FaseItem>();
}


public class FaseItem
{
    public string Fase { get; set; } = string.Empty;
    public string EstadoFase { get; set; } = string.Empty;
    public string FechaOperacion { get; set; } = string.Empty;
    public string Comentario { get; set; } = string.Empty;
}
