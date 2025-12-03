namespace SmartWarehouseAPI.Models.Request
{
    public class ItemPedidoRequest
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }

}