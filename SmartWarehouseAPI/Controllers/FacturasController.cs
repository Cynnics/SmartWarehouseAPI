using Microsoft.AspNetCore.Mvc;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartWarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            factura.FechaEmision = DateTime.Now;
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostFactura), new { id = factura.IdFactura }, factura);
        }

        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturasPorPedido(int idPedido)
        {
            return await _context.Facturas
                .Where(f => f.IdPedido == idPedido)
                .ToListAsync();
        }
    }
}
