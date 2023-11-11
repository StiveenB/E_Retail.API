using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class TipoUsuarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TipoUsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoUsuario>>> GetTiposUsuario()
    {
        return await _context.tipo_usuario.ToListAsync();
    }

    [HttpGet("{tusu_id}")]
    public async Task<ActionResult<TipoUsuario>> GetTipoUsuario(int tusu_id)
    {
        var tipoUsuario = await _context.tipo_usuario.FindAsync(tusu_id);

        if (tipoUsuario == null)
        {
            return NotFound();
        }

        return tipoUsuario;
    }

    [HttpPost]
    public async Task<ActionResult<TipoUsuario>> PostTipoUsuario(TipoUsuario tipoUsuario)
    {
        _context.tipo_usuario.Add(tipoUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTipoUsuario", new { tusu_id = tipoUsuario.tusu_id }, tipoUsuario);
    }

    [HttpPut("{tusu_id}")]
    public async Task<IActionResult> PutTipoUsuario(int tusu_id, TipoUsuario tipoUsuario)
    {
        if (tusu_id != tipoUsuario.tusu_id)
        {
            return BadRequest();
        }

        _context.Entry(tipoUsuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TipoUsuarioExists(tusu_id))
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

    [HttpDelete("{tusu_id}")]
    public async Task<IActionResult> DeleteTipoUsuario(int tusu_id)
    {
        var tipoUsuario = await _context.tipo_usuario.FindAsync(tusu_id);
        if (tipoUsuario == null)
        {
            return NotFound();
        }

        _context.tipo_usuario.Remove(tipoUsuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TipoUsuarioExists(int tusu_id)
    {
        return _context.tipo_usuario.Any(e => e.tusu_id == tusu_id);
    }
}
