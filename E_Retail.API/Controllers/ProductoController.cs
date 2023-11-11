using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        return await _context.producto
            .Include(p => p.TipoProducto)
            .Include(p => p.Usuario)
            .ToListAsync();
    }

    [HttpGet("{pro_id}")]
    public async Task<ActionResult<Producto>> GetProducto(string pro_id)
    {
        // Incluye las propiedades de navegación para cargar los objetos relacionados
        var producto = await _context.producto
            .Include(p => p.TipoProducto)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.pro_id == pro_id);

        if (producto == null)
        {
            return NotFound();
        }

        return producto;
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.producto.Add(producto);
        await _context.SaveChangesAsync();

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(producto)
            .Reference(p => p.TipoProducto)
            .LoadAsync();

        await _context.Entry(producto)
            .Reference(p => p.Usuario)
            .LoadAsync();

        return CreatedAtAction("GetProducto", new { pro_id = producto.pro_id }, producto);
    }

    [HttpPut("{pro_id}")]
    public async Task<IActionResult> PutProducto(string pro_id, Producto producto)
    {
        if (pro_id != producto.pro_id)
        {
            return BadRequest();
        }

        _context.Entry(producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(pro_id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Incluye las propiedades de navegación para cargar los objetos relacionados
        await _context.Entry(producto)
            .Reference(p => p.TipoProducto)
            .LoadAsync();

        await _context.Entry(producto)
            .Reference(p => p.Usuario)
            .LoadAsync();

        return NoContent();
    }

    [HttpDelete("{pro_id}")]
    public async Task<IActionResult> DeleteProducto(string pro_id)
    {
        var producto = await _context.producto.FindAsync(pro_id);
        if (producto == null)
        {
            return NotFound();
        }

        _context.producto.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductoExists(string pro_id)
    {
        return _context.producto.Any(e => e.pro_id == pro_id);
    }
}
