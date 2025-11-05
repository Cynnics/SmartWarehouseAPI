namespace SmartWarehouseAPI.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int? IdRepartidor { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}
