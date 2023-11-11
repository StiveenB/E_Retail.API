using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class DetalleVentaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DetalleVentaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetalleVentas()
    {
        return await _context.detalle_venta.ToListAsync();
    }

    [HttpGet("{dven_id}")]
    public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(string dven_id)
    {
        var detalleVenta = await _context.detalle_venta.FindAsync(dven_id);

        if (detalleVenta == null)
        {
            return NotFound();
        }

        return detalleVenta;
    }

    [HttpPost]
    public async Task<ActionResult<DetalleVenta>> PostDetalleVenta(DetalleVenta detalleVenta)
    {
        _context.detalle_venta.Add(detalleVenta);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDetalleVenta", new { dven_id = detalleVenta.dven_id }, detalleVenta);
    }

    [HttpPut("{dven_id}")]
    public async Task<IActionResult> PutDetalleVenta(string dven_id, DetalleVenta detalleVenta)
    {
        if (dven_id != detalleVenta.dven_id)
        {
            return BadRequest();
        }

        _context.Entry(detalleVenta).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalleVentaExists(dven_id))
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

    [HttpDelete("{dven_id}")]
    public async Task<IActionResult> DeleteDetalleVenta(string dven_id)
    {
        var detalleVenta = await _context.detalle_venta.FindAsync(dven_id);
        if (detalleVenta == null)
        {
            return NotFound();
        }

        _context.detalle_venta.Remove(detalleVenta);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DetalleVentaExists(string dven_id)
    {
        return _context.detalle_venta.Any(e => e.dven_id == dven_id);
    }
}
