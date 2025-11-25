using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Models;

namespace SmartWarehouseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas principales
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<RutaEntrega> RutasEntrega { get; set; }

        // 🔥 Tablas que faltaban
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<RutaPedido> RutasPedido { get; set; }
        public DbSet<UbicacionRepartidor> UbicacionesRepartidor { get; set; }
        public DbSet<Albaran> Albaranes { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // CONFIGURACIONES ESPECIALES EF
            // -----------------------------

            // 📌 TABLA RutaPedido — no tiene clave primaria real
            modelBuilder.Entity<RutaPedido>()
                .HasKey(rp => new { rp.IdRuta, rp.IdPedido });

            // 📌 TABLA DetallePedido — clave primaria simple
            modelBuilder.Entity<DetallePedido>()
                .HasKey(dp => dp.IdDetalle);

            // 📌 TABLA UbicacionRepartidor
            modelBuilder.Entity<UbicacionRepartidor>()
                .HasKey(u => u.IdUbicacion);

            // 📌 TABLA Albaran
            modelBuilder.Entity<Albaran>()
                .HasKey(a => a.IdAlbaran);

            // -------------------------------------
            // OPCIONAL: nombres automáticos correctos
            // -------------------------------------
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<Producto>().ToTable("producto");
            modelBuilder.Entity<Pedido>().ToTable("pedido");
            modelBuilder.Entity<Factura>().ToTable("factura");
            modelBuilder.Entity<RutaEntrega>().ToTable("rutaentrega");
            modelBuilder.Entity<DetallePedido>().ToTable("detallepedido");
            modelBuilder.Entity<RutaPedido>().ToTable("rutapedido");
            modelBuilder.Entity<UbicacionRepartidor>().ToTable("ubicacionrepartidor");
            modelBuilder.Entity<Albaran>().ToTable("albaran");
        }
    }
}
