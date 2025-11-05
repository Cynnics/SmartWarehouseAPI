using Microsoft.AspNetCore.Mvc;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartWarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RutasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RutaEntrega>>> GetRutas()
        {
            return await _context.RutasEntrega.ToListAsync();
        }

        [HttpGet("repartidor/{id}")]
        public async Task<ActionResult<IEnumerable<RutaEntrega>>> GetRutasPorRepartidor(int id)
        {
            return await _context.RutasEntrega
                .Where(r => r.IdRepartidor == id)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RutaEntrega>> PostRuta(RutaEntrega ruta)
        {
            _context.RutasEntrega.Add(ruta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostRuta), new { id = ruta.IdRuta }, ruta);
        }
    }
}
