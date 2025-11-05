using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Models;

namespace SmartWarehouseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<RutaEntrega> RutasEntrega { get; set; }



    }
}

