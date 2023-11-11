using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class SeguridadController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SeguridadController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seguridad>>> GetSeguridades()
    {
        // Incluye la propiedad de navegación para cargar el objeto relacionado
        return await _context.seguridad
            .Include(s => s.TipoAccion)
            .ToListAsync();
    }

    [HttpGet("{seg_id}")]
    public async Task<ActionResult<Seguridad>> GetSeguridad(int seg_id)
    {
        // Incluye la propiedad de navegación para cargar el objeto relacionado
        var seguridad = await _context.seguridad
            .Include(s => s.TipoAccion)
            .FirstOrDefaultAsync(s => s.seg_id == seg_id);

        if (seguridad == null)
        {
            return NotFound();
        }

        return seguridad;
    }

    [HttpPost]
    public async Task<ActionResult<Seguridad>> PostSeguridad(Seguridad seguridad)
    {
        _context.seguridad.Add(seguridad);
        await _context.SaveChangesAsync();

        // Incluye la propiedad de navegación para cargar el objeto relacionado
        await _context.Entry(seguridad)
            .Reference(s => s.TipoAccion)
            .LoadAsync();

        return CreatedAtAction("GetSeguridad", new { seg_id = seguridad.seg_id }, seguridad);
    }

    [HttpPut("{seg_id}")]
    public async Task<IActionResult> PutSeguridad(int seg_id, Seguridad seguridad)
    {
        if (seg_id != seguridad.seg_id)
        {
            return BadRequest();
        }

        _context.Entry(seguridad).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SeguridadExists(seg_id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Incluye la propiedad de navegación para cargar el objeto relacionado
        await _context.Entry(seguridad)
            .Reference(s => s.TipoAccion)
            .LoadAsync();

        return NoContent();
    }

    [HttpDelete("{seg_id}")]
    public async Task<IActionResult> DeleteSeguridad(int seg_id)
    {
        var seguridad = await _context.seguridad.FindAsync(seg_id);
        if (seguridad == null)
        {
            return NotFound();
        }

        _context.seguridad.Remove(seguridad);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SeguridadExists(int seg_id)
    {
        return _context.seguridad.Any(e => e.seg_id == seg_id);
    }
}
