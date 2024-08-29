using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeitekRF.API.Data;
using NeitekRF.API.Models;

namespace NeitekRF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TareasController : ControllerBase
{
    private readonly RfContext _contexto;

    public TareasController(RfContext contexto)
    {
        _contexto = contexto;
    }

    // GET: api/Tareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
    {
        return await _contexto.Tareas.Where(t => t.IsActive).ToListAsync();
    }

    // GET: api/Tareas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarea>> GetTarea(int id)
    {
        var tarea = await _contexto.Tareas.FindAsync(id);

        if (tarea == null || !tarea.IsActive)
        {
            return NotFound();
        }

        return tarea;
    }

    // PUT: api/Tareas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarea(int id, Tarea tarea)
    {
        if (id != tarea.Id)
        {
            return BadRequest();
        }

        _contexto.Entry(tarea).State = EntityState.Modified;

        try
        {
            await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TareaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Tareas
    [HttpPost]
    public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
    {
        tarea.IsActive = true; // Aseguramos que la tarea se cree como activa
        _contexto.Tareas.Add(tarea);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction("GetTarea", new { id = tarea.Id }, tarea);
    }

    // DELETE: api/Tareas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarea(int id)
    {
        var tarea = await _contexto.Tareas.FindAsync(id);
        if (tarea == null || !tarea.IsActive)
        {
            return NotFound();
        }

        tarea.IsActive = false;

        _contexto.Entry(tarea).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    private bool TareaExists(int id)
    {
        return _contexto.Tareas.Any(e => e.Id == id && e.IsActive);
    }
}