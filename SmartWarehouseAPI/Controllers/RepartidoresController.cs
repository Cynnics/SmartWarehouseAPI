using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class RepartidoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public RepartidoresController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Repartidores
    [HttpGet]
    public async Task<IActionResult> GetRepartidores()
    {
        var repartidores = await _context.Usuarios
            .Where(u => u.Rol == "repartidor")
            .Select(u => new {
                u.IdUsuario,
                u.Nombre
            })
            .ToListAsync();

        return Ok(repartidores);
    }
}
