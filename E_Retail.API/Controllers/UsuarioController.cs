using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        return await _context.usuario
            .Include(u => u.Persona)
            .Include(u => u.TipoUsuario)
            .Include(u => u.Negocio)
            .ToListAsync();
    }

    [HttpGet("{usu_id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(string usu_id)
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        var usuario = await _context.usuario
            .Include(u => u.Persona)
            .Include(u => u.TipoUsuario)
            .Include(u => u.Negocio)
            .FirstOrDefaultAsync(u => u.usu_id == usu_id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.usuario.Add(usuario);
        await _context.SaveChangesAsync();

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(usuario)
            .Reference(u => u.Persona)
            .LoadAsync();

        await _context.Entry(usuario)
            .Reference(u => u.TipoUsuario)
            .LoadAsync();

        await _context.Entry(usuario)
            .Reference(u => u.Negocio)
            .LoadAsync();

        return CreatedAtAction("GetUsuario", new { usu_id = usuario.usu_id }, usuario);
    }

    [HttpPut("{usu_id}")]
    public async Task<IActionResult> PutUsuario(string usu_id, Usuario usuario)
    {
        if (usu_id != usuario.usu_id)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(usu_id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(usuario)
            .Reference(u => u.Persona)
            .LoadAsync();

        await _context.Entry(usuario)
            .Reference(u => u.TipoUsuario)
            .LoadAsync();

        await _context.Entry(usuario)
            .Reference(u => u.Negocio)
            .LoadAsync();

        return NoContent();
    }

    [HttpDelete("{usu_id}")]
    public async Task<IActionResult> DeleteUsuario(string usu_id)
    {
        var usuario = await _context.usuario.FindAsync(usu_id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.usuario.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(string usu_id)
    {
        return _context.usuario.Any(e => e.usu_id == usu_id);
    }
}
