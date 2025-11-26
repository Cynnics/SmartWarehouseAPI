using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UbicacionesRepartidorController : ControllerBase
{
    private readonly AppDbContext _context;
    public UbicacionesRepartidorController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Ubicaciones
    [HttpGet]
    [Authorize(Roles = "admin,empleado")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.UbicacionesRepartidor
            .OrderByDescending(u => u.FechaHora)
            .ToListAsync();

        return Ok(data);
    }

    // GET: api/Ubicaciones/repartidor/3
    [HttpGet("repartidor/{idRepartidor}")]
    [Authorize(Roles = "admin,empleado,repartidor")]
    public async Task<IActionResult> GetByRepartidor(int idRepartidor)
    {
        var data = await _context.UbicacionesRepartidor
            .Where(u => u.IdRepartidor == idRepartidor)
            .OrderByDescending(u => u.FechaHora)
            .ToListAsync();

        return Ok(data);
    }

    // GET: api/Ubicaciones/{id}
    [HttpGet("{id}")]
    [Authorize(Roles = "admin,empleado,repartidor")]
    public async Task<IActionResult> GetById(int id)
    {
        var ubic = await _context.UbicacionesRepartidor.FindAsync(id);
        if (ubic == null) return NotFound();
        return Ok(ubic);
    }

    // GET último punto
    [HttpGet("last/repartidor/{idRepartidor}")]
    [Authorize(Roles = "admin,empleado,repartidor")]
    public async Task<IActionResult> GetLast(int idRepartidor)
    {
        var last = await _context.UbicacionesRepartidor
            .Where(u => u.IdRepartidor == idRepartidor)
            .OrderByDescending(u => u.FechaHora)
            .FirstOrDefaultAsync();

        return last == null ? NotFound() : Ok(last);
    }

    // POST (Android y escritorio)
    [HttpPost]
    [Authorize(Roles = "admin,empleado,repartidor")]
    public async Task<IActionResult> Create([FromBody] UbicacionRepartidor dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new UbicacionRepartidor
        {
            IdRepartidor = dto.IdRepartidor,
            Latitud = dto.Latitud,
            Longitud = dto.Longitud,
            FechaHora = dto.FechaHora == default ? DateTime.Now : dto.FechaHora
        };

        _context.UbicacionesRepartidor.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.IdUbicacion }, entity);
    }

    // DELETE
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var ubic = await _context.UbicacionesRepartidor.FindAsync(id);
        if (ubic == null) return NotFound();

        _context.UbicacionesRepartidor.Remove(ubic);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
