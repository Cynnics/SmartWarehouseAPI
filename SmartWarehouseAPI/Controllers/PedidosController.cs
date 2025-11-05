using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;

namespace SmartWarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔒 Solo accesible con token JWT
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // ✅ GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound(new { message = $"No se encontró el pedido con ID {id}" });

            return pedido;
        }

        // ✅ GET: api/pedidos/repartidor/3
        // Obtiene pedidos asignados a un repartidor específico (para la app Android)
        [HttpGet("repartidor/{idRepartidor}")]
        [Authorize(Roles = "REPARTIDOR,ADMIN,EMPLEADO")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosPorRepartidor(int idRepartidor)
        {
            var pedidos = await _context.Pedidos
                .Where(p => p.IdRepartidor == idRepartidor)
                .ToListAsync();

            return pedidos;
        }

        // ✅ POST: api/pedidos
        [HttpPost]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            pedido.FechaCreacion = DateTime.Now;
            pedido.Estado = "Pendiente";

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }

        // ✅ PUT: api/pedidos/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,EMPLEADO,REPARTIDOR")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
                return BadRequest(new { message = "El ID del pedido no coincide." });

            var pedidoExistente = await _context.Pedidos.FindAsync(id);
            if (pedidoExistente == null)
                return NotFound(new { message = "Pedido no encontrado." });

            pedidoExistente.IdCliente = pedido.IdCliente;
            pedidoExistente.IdRepartidor = pedido.IdRepartidor;
            pedidoExistente.Estado = pedido.Estado;
            pedidoExistente.FechaEntrega = pedido.FechaEntrega;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/pedidos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido no encontrado." });

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ PATCH: api/pedidos/5/estado
        // Permite que un repartidor cambie el estado del pedido (por ejemplo, "Entregado")
        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "REPARTIDOR,ADMIN,EMPLEADO")]
        public async Task<IActionResult> CambiarEstadoPedido(int id, [FromBody] string nuevoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido no encontrado." });

            pedido.Estado = nuevoEstado;

            if (nuevoEstado.ToLower() == "entregado")
                pedido.FechaEntrega = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Estado del pedido {id} actualizado a '{nuevoEstado}'" });
        }
    }
}
