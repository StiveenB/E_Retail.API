using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class TipoAccionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TipoAccionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoAccion>>> GetTiposAccion()
    {
        return await _context.tipo_accion.ToListAsync();
    }

    [HttpGet("{tacc_id}")]
    public async Task<ActionResult<TipoAccion>> GetTipoAccion(int tacc_id)
    {
        var tipoAccion = await _context.tipo_accion.FindAsync(tacc_id);

        if (tipoAccion == null)
        {
            return NotFound();
        }

        return tipoAccion;
    }

    [HttpPost]
    public async Task<ActionResult<TipoAccion>> PostTipoAccion(TipoAccion tipoAccion)
    {
        _context.tipo_accion.Add(tipoAccion);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTipoAccion", new { tacc_id = tipoAccion.tacc_id }, tipoAccion);
    }

    [HttpPut("{tacc_id}")]
    public async Task<IActionResult> PutTipoAccion(int tacc_id, TipoAccion tipoAccion)
    {
        if (tacc_id != tipoAccion.tacc_id)
        {
            return BadRequest();
        }

        _context.Entry(tipoAccion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TipoAccionExists(tacc_id))
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

    [HttpDelete("{tacc_id}")]
    public async Task<IActionResult> DeleteTipoAccion(int tacc_id)
    {
        var tipoAccion = await _context.tipo_accion.FindAsync(tacc_id);
        if (tipoAccion == null)
        {
            return NotFound();
        }

        _context.tipo_accion.Remove(tipoAccion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TipoAccionExists(int tacc_id)
    {
        return _context.tipo_accion.Any(e => e.tacc_id == tacc_id);
    }
}


