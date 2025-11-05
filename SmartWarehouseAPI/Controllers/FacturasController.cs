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

        [HttpGet]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetPorPedido(int idPedido)
        {
            var facturas = await _context.Facturas.Where(f => f.IdPedido == idPedido).ToListAsync();
            return facturas;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            factura.FechaEmision = DateTime.Now;
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPorPedido), new { idPedido = factura.IdPedido }, factura);
        }
    }
}
