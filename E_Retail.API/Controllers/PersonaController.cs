using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PersonaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
    {
        return await _context.persona.ToListAsync();
    }

    [HttpGet("{cedula}")]
    public async Task<ActionResult<Persona>> GetPersona(string cedula)
    {
        var persona = await _context.persona.FindAsync(cedula);

        if (persona == null)
        {
            return NotFound();
        }

        return persona;
    }

    [HttpPost]
    public async Task<ActionResult<Persona>> PostPersona(Persona persona)
    {
        _context.persona.Add(persona);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPersona", new { cedula = persona.per_cedula }, persona);
    }

    [HttpPut("{cedula}")]
    public async Task<IActionResult> PutPersona(string cedula, Persona persona)
    {
        if (cedula != persona.per_cedula)
        {
            return BadRequest();
        }

        _context.Entry(persona).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonaExists(cedula))
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

    [HttpDelete("{cedula}")]
    public async Task<IActionResult> DeletePersona(string cedula)
    {
        var persona = await _context.persona.FindAsync(cedula);
        if (persona == null)
        {
            return NotFound();
        }

        _context.persona.Remove(persona);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonaExists(string cedula)
    {
        return _context.persona.Any(e => e.per_cedula == cedula);
    }
}
