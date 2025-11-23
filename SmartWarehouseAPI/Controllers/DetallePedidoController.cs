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
    public class DetallePedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetallePedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetalles()
        {
            return await _context.DetallesPedido.ToListAsync();
        }

        // GET por pedido
        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetPorPedido(int idPedido)
        {
            return await _context.DetallesPedido
                .Where(d => d.IdPedido == idPedido)
                .ToListAsync();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Crear(DetallePedido detalle)
        {
            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();
            return Ok(detalle);
        }

        // PATCH (editar cantidad o subtotal)
        [HttpPatch("{id}")]
        public async Task<ActionResult> Editar(int id, [FromBody] DetallePedido cambios)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);
            if (detalle == null) return NotFound();

            detalle.Cantidad = cambios.Cantidad;
            detalle.Subtotal = cambios.Subtotal;

            await _context.SaveChangesAsync();
            return Ok(detalle);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Borrar(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);
            if (detalle == null) return NotFound();

            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

