using System.ComponentModel.DataAnnotations;

public class Pedido
{
    [Key]   // ← AÑADIR ESTO
    public int IdPedido { get; set; }

    public int IdCliente { get; set; }
    public int? IdRepartidor { get; set; }
    public string Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
}
