using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;
using SmartWarehouseAPI.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize] // token obligatorio
public class UbicacionesController : ControllerBase
{
    private readonly AppDbContext _context;
    public UbicacionesController(AppDbContext context) { _context = context; }

    // POST: api/Ubicaciones
    // Repartidor sube su ubicación (Android)
    [HttpPost]
    [Authorize(Roles = "repartidor,admin")]
    public async Task<IActionResult> Create([FromBody] UbicacionRepartidor dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ubic = new UbicacionRepartidor
        {
            IdRepartidor = dto.IdRepartidor,
            Latitud = dto.Latitud,
            Longitud = dto.Longitud,
            FechaHora = dto.FechaHora != default(DateTime) ? dto.FechaHora : DateTime.Now
        };

        _context.UbicacionesRepartidor.Add(ubic);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = ubic.IdUbicacion }, ubic);
    }

    // GET: api/Ubicaciones/repartidor/3 (todas las ubicaciones)
    [HttpGet("repartidor/{idRepartidor}")]
    [Authorize(Roles = "repartidor,admin,empleado")]
    public async Task<IActionResult> GetByRepartidor(int idRepartidor)
    {
        var datos = await _context.UbicacionesRepartidor
            .Where(u => u.IdRepartidor == idRepartidor)
            .OrderByDescending(u => u.FechaHora)
            .ToListAsync();
        return Ok(datos);
    }

    // GET: api/Ubicaciones/last/repartidor/3 -> última ubicación conocida
    [HttpGet("last/repartidor/{idRepartidor}")]
    [Authorize(Roles = "repartidor,admin,empleado")]
    public async Task<IActionResult> GetLast(int idRepartidor)
    {
        var last = await _context.UbicacionesRepartidor
            .Where(u => u.IdRepartidor == idRepartidor)
            .OrderByDescending(u => u.FechaHora)
            .FirstOrDefaultAsync();
        if (last == null) return NotFound();
        return Ok(last);
    }

    // GET by id
    [HttpGet("{id}")]
    [Authorize(Roles = "repartidor,admin,empleado")]
    public async Task<IActionResult> GetById(int id)
    {
        var ubic = await _context.UbicacionesRepartidor.FindAsync(id);
        if (ubic == null) return NotFound();
        return Ok(ubic);
    }
}
