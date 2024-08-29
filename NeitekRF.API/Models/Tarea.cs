namespace NeitekRF.API.Models;

public class Tarea
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? FechaCreada { get; set; } = DateTime.Now;
    public EstadoTareaEnum Estado { get; set; } = EstadoTareaEnum.Abierta;
    public int MetaId { get; set; }
    public bool IsActive { get; set; } = true;
}
