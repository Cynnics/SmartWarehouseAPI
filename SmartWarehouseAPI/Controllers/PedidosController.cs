using Microsoft.AspNetCore.Mvc;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartWarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();
            return pedido;
        }

        // PUT: api/pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido) return BadRequest();

            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }

        // DELETE: api/pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
