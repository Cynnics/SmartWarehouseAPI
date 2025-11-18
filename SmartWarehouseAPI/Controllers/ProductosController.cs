using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;

namespace SmartWarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            return Ok(await _context.Productos.ToListAsync());
        }

        // GET: api/productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var prod = await _context.Productos.FindAsync(id);
            if (prod == null) return NotFound();
            return Ok(prod);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return Ok(producto);
        }

        // PUT: api/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProducto(int id, Producto producto)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null) return NotFound();   

            p.Nombre = producto.Nombre;
            p.Descripcion = producto.Descripcion;
            p.Precio = producto.Precio;
            p.Stock = producto.Stock;

            await _context.SaveChangesAsync();
            return Ok(p);
        }

        // DELETE: api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var prod = await _context.Productos.FindAsync(id);
            if (prod == null) return NotFound();

            _context.Productos.Remove(prod);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}