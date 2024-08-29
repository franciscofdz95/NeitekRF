using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeitekRF.API.Data;
using NeitekRF.API.Models;

namespace NeitekRF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetasController : ControllerBase
{
    private readonly RfContext _contexto;

    public MetasController(RfContext contexto)
    {
        _contexto = contexto;
    }

    // GET: api/Metas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
    {
        return await _contexto.Metas.Include(m => m.Tareas).ToListAsync();
    }

    // GET: api/Metas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Meta>> GetMeta(int id)
    {
        var meta = await _contexto.Metas.Include(m => m.Tareas)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (meta == null)
        {
            return NotFound();
        }

        return meta;
    }

    // PUT: api/Metas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeta(int id, Meta meta)
    {
        if (id != meta.Id)
        {
            return BadRequest();
        }

        _contexto.Entry(meta).State = EntityState.Modified;

        try
        {
            await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MetaExists(id))
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

    // POST: api/Metas
    [HttpPost]
    public async Task<ActionResult<Meta>> PostMeta(Meta meta)
    {
        _contexto.Metas.Add(meta);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction("GetMeta", new { id = meta.Id }, meta);
    }

    // DELETE: api/Metas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeta(int id)
    {
        var meta = await _contexto.Metas.FindAsync(id);
        if (meta == null)
        {
            return NotFound();
        }

        meta.IsActive = false;

        _contexto.Entry(meta).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    private bool MetaExists(int id)
    {
        return _contexto.Metas.Any(e => e.Id == id);
    }
}
