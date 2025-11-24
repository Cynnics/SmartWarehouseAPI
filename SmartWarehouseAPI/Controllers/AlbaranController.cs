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
    public class AlbaranesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlbaranesController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/albaranes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Albaran>>> GetAlbaranes()
        {
            return await _context.Albaranes.ToListAsync();
        }

        // 🔹 GET: api/albaranes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Albaran>> GetAlbaran(int id)
        {
            var albaran = await _context.Albaranes.FindAsync(id);

            if (albaran == null)
                return NotFound(new { message = "Albarán no encontrado." });

            return albaran;
        }

        // 🔹 GET: api/albaranes/pedido/3
        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<Albaran>> GetAlbaranPorPedido(int idPedido)
        {
            var albaran = await _context.Albaranes
                .FirstOrDefaultAsync(a => a.IdPedido == idPedido);

            if (albaran == null)
                return NotFound(new { message = "Este pedido no tiene albarán." });

            return albaran;
        }

        // 🔹 POST: api/albaranes   (Crear albarán)
        [HttpPost]
        [Authorize(Roles = "admin,empleado")]
        public async Task<ActionResult<Albaran>> PostAlbaran(Albaran model)
        {
            // validar que el pedido exista
            var pedido = await _context.Pedidos.FindAsync(model.IdPedido);
            if (pedido == null)
                return BadRequest(new { message = "El pedido no existe." });

            model.FechaGeneracion = DateTime.Now;
            model.Estado = "generado";

            _context.Albaranes.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbaran), new { id = model.IdAlbaran }, model);
        }

        // 🔹 PATCH: api/albaranes/5/estado
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var albaran = await _context.Albaranes.FindAsync(id);
            if (albaran == null)
                return NotFound(new { message = "Albarán no encontrado." });

            albaran.Estado = nuevoEstado;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Estado del albarán actualizado a {nuevoEstado}" });
        }

        // 🔹 DELETE: api/albaranes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,empleado")]
        public async Task<IActionResult> DeleteAlbaran(int id)
        {
            var albaran = await _context.Albaranes.FindAsync(id);
            if (albaran == null)
                return NotFound(new { message = "Albarán no encontrado." });

            _context.Albaranes.Remove(albaran);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
