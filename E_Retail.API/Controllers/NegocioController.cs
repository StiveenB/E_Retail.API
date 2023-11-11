using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class NegocioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NegocioController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Negocio>>> GetNegocios()
    {
        return await _context.negocio.ToListAsync();
    }

    [HttpGet("{neg_id}")]
    public async Task<ActionResult<Negocio>> GetNegocio(string neg_id)
    {
        var negocio = await _context.negocio.FindAsync(neg_id);

        if (negocio == null)
        {
            return NotFound();
        }

        return negocio;
    }

    [HttpPost]
    public async Task<ActionResult<Negocio>> PostNegocio(Negocio negocio)
    {
        _context.negocio.Add(negocio);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNegocio", new { neg_id = negocio.neg_id }, negocio);
    }

    [HttpPut("{neg_id}")]
    public async Task<IActionResult> PutNegocio(string neg_id, Negocio negocio)
    {
        if (neg_id != negocio.neg_id)
        {
            return BadRequest();
        }

        _context.Entry(negocio).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NegocioExists(neg_id))
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

    [HttpDelete("{neg_id}")]
    public async Task<IActionResult> DeleteNegocio(string neg_id)
    {
        var negocio = await _context.negocio.FindAsync(neg_id);
        if (negocio == null)
        {
            return NotFound();
        }

        _context.negocio.Remove(negocio);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NegocioExists(string neg_id)
    {
        return _context.negocio.Any(e => e.neg_id == neg_id);
    }
}
