using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VentasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ventas>>> GetVentas()
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        return await _context.ventas
            .Include(v => v.DetalleVenta)
            .Include(v => v.Usuario)
            .ToListAsync();
    }

    [HttpGet("{ven_id}")]
    public async Task<ActionResult<Ventas>> GetVenta(string ven_id)
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        var venta = await _context.ventas
            .Include(v => v.DetalleVenta)
            .Include(v => v.Usuario)
            .FirstOrDefaultAsync(v => v.ven_id == ven_id);

        if (venta == null)
        {
            return NotFound();
        }

        return venta;
    }

    [HttpPost]
    public async Task<ActionResult<Ventas>> PostVenta(Ventas venta)
    {
        _context.ventas.Add(venta);
        await _context.SaveChangesAsync();

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(venta)
            .Reference(v => v.DetalleVenta)
            .LoadAsync();

        await _context.Entry(venta)
            .Reference(v => v.Usuario)
            .LoadAsync();

        return CreatedAtAction("GetVenta", new { ven_id = venta.ven_id }, venta);
    }

    [HttpPut("{ven_id}")]
    public async Task<IActionResult> PutVenta(string ven_id, Ventas venta)
    {
        if (ven_id != venta.ven_id)
        {
            return BadRequest();
        }

        _context.Entry(venta).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VentaExists(ven_id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(venta)
            .Reference(v => v.DetalleVenta)
            .LoadAsync();

        await _context.Entry(venta)
            .Reference(v => v.Usuario)
            .LoadAsync();

        return NoContent();
    }

    [HttpDelete("{ven_id}")]
    public async Task<IActionResult> DeleteVenta(string ven_id)
    {
        var venta = await _context.ventas.FindAsync(ven_id);
        if (venta == null)
        {
            return NotFound();
        }

        _context.ventas.Remove(venta);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VentaExists(string ven_id)
    {
        return _context.ventas.Any(e => e.ven_id == ven_id);
    }
}
