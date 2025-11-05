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
    public class RutasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RutasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<ActionResult<IEnumerable<RutaEntrega>>> GetRutas()
        {
            return await _context.RutasEntrega.ToListAsync();
        }

        [HttpGet("repartidor/{id}")]
        [Authorize(Roles = "REPARTIDOR,ADMIN,EMPLEADO")]
        public async Task<ActionResult<IEnumerable<RutaEntrega>>> GetRutasPorRepartidor(int id)
        {
            return await _context.RutasEntrega.Where(r => r.IdRepartidor == id).ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<ActionResult<RutaEntrega>> PostRuta(RutaEntrega ruta)
        {
            ruta.Estado = "Planificada";
            ruta.FechaRuta = DateTime.Now;
            _context.RutasEntrega.Add(ruta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRutasPorRepartidor), new { id = ruta.IdRepartidor }, ruta);
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "ADMIN,EMPLEADO,REPARTIDOR")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var ruta = await _context.RutasEntrega.FindAsync(id);
            if (ruta == null)
                return NotFound();

            ruta.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Ruta {id} actualizada a '{nuevoEstado}'" });
        }
    }
}
