using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProveedorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
    {
        return await _context.proveedor.ToListAsync();
    }

    [HttpGet("{prov_id}")]
    public async Task<ActionResult<Proveedor>> GetProveedor(string prov_id)
    {
        var proveedor = await _context.proveedor.FindAsync(prov_id);

        if (proveedor == null)
        {
            return NotFound();
        }

        return proveedor;
    }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
    {
        _context.proveedor.Add(proveedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProveedor", new { prov_id = proveedor.prov_id }, proveedor);
    }

    [HttpPut("{prov_id}")]
    public async Task<IActionResult> PutProveedor(string prov_id, Proveedor proveedor)
    {
        if (prov_id != proveedor.prov_id)
        {
            return BadRequest();
        }

        _context.Entry(proveedor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProveedorExists(prov_id))
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

    [HttpDelete("{prov_id}")]
    public async Task<IActionResult> DeleteProveedor(string prov_id)
    {
        var proveedor = await _context.proveedor.FindAsync(prov_id);
        if (proveedor == null)
        {
            return NotFound();
        }

        _context.proveedor.Remove(proveedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProveedorExists(string prov_id)
    {
        return _context.proveedor.Any(e => e.prov_id == prov_id);
    }
}
