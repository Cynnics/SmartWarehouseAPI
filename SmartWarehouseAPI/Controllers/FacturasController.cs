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
    public class FacturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturasController(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Obtener todas las facturas (solo activas)
        [HttpGet]
        [Authorize(Roles = "admin,empleado")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas
                .Where(f => f.Activo == true)
                .ToListAsync();
        }

        // 📌 Obtener facturas por pedido
        [HttpGet("pedido/{idPedido}")]
        [Authorize(Roles = "admin,empleado")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetPorPedido(int idPedido)
        {
            var facturas = await _context.Facturas
                .Where(f => f.IdPedido == idPedido && f.Activo == true)
                .ToListAsync();

            if (!facturas.Any())
                return NotFound("No existen facturas para este pedido.");

            return Ok(facturas);
        }

        // 📌 Crear factura
        [HttpPost]
        [Authorize(Roles = "admin,empleado")]
        public async Task<ActionResult<Factura>> PostFactura([FromBody] Factura factura)
        {
            var pedido = await _context.Pedidos.FindAsync(factura.IdPedido);
            if (pedido == null)
                return BadRequest(new { message = "El pedido no existe." });

            if (pedido.Estado != "entregado")
                return BadRequest(new { message = "Solo se pueden facturar pedidos entregados." });

            if (pedido.FechaEntrega == null)
                return BadRequest(new { message = "El pedido no tiene fecha de entrega." });

            factura.FechaEmision = DateTime.Now;

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return Ok(factura);
        }


        // 📌 Eliminar factura (solo admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null) return NotFound();

            // Soft delete en lugar de borrar
            factura.Activo = false;
            await _context.SaveChangesAsync();

            return Ok("Factura marcada como eliminada.");
        }
    }
}
