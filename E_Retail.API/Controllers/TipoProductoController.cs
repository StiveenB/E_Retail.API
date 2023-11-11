using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Retail.API.Data;
using E_Retail.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Retail.API.Data.TuProyecto.Data;

[ApiController]
[Route("api/[controller]")]
public class TipoProductoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TipoProductoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoProducto>>> GetTiposProducto()
    {
        return await _context.tipo_producto.ToListAsync();
    }

    [HttpGet("{tpro_id}")]
    public async Task<ActionResult<TipoProducto>> GetTipoProducto(string tpro_id)
    {
        var tipoProducto = await _context.tipo_producto.FindAsync(tpro_id);

        if (tipoProducto == null)
        {
            return NotFound();
        }

        return tipoProducto;
    }

    [HttpPost]
    public async Task<ActionResult<TipoProducto>> PostTipoProducto(TipoProducto tipoProducto)
    {
        _context.tipo_producto.Add(tipoProducto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTipoProducto", new { tpro_id = tipoProducto.tpro_id }, tipoProducto);
    }

    [HttpPut("{tpro_id}")]
    public async Task<IActionResult> PutTipoProducto(string tpro_id, TipoProducto tipoProducto)
    {
        if (tpro_id != tipoProducto.tpro_id)
        {
            return BadRequest();
        }

        _context.Entry(tipoProducto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TipoProductoExists(tpro_id))
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

    [HttpDelete("{tpro_id}")]
    public async Task<IActionResult> DeleteTipoProducto(string tpro_id)
    {
        var tipoProducto = await _context.tipo_producto.FindAsync(tpro_id);
        if (tipoProducto == null)
        {
            return NotFound();
        }

        _context.tipo_producto.Remove(tipoProducto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TipoProductoExists(string tpro_id)
    {
        return _context.tipo_producto.Any(e => e.tpro_id == tpro_id);
    }
}
