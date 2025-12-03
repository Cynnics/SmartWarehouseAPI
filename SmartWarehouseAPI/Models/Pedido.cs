using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pedido
{
    [Key]
    public int IdPedido { get; set; }

    public int IdCliente { get; set; }
    public int? IdRepartidor { get; set; }

    public string Estado { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? DireccionEntrega { get; set; }

    // 🔥 NUEVOS CAMPOS AÑADIDOS
    public string? Ciudad { get; set; }

    public string? CodigoPostal { get; set; }

    [Column(TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column(TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    public string? Notas { get; set; }
}