namespace NeitekRF.API.Models;

public class Meta
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public ICollection<Tarea>? Tareas { get; set; }
    public bool IsActive { get; set; } = true;

    public int TotalTareas => Tareas?.Count ?? 0;
    public int TareasCompletadas => Tareas?.Count(t => t.Estado == EstadoTareaEnum.Completada) ?? 0;
    public double PorcientoCumplimiento => TotalTareas == 0 ? 0 : (double)TareasCompletadas / TotalTareas * 100;
}
