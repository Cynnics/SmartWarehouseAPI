namespace SmartWarehouseAPI.Models.Request
{
    public class CrearPedidoRequest
    {
        public int IdCliente { get; set; }
        public string DireccionEntrega { get; set; }
        public string Notas { get; set; }
        public List<ItemPedidoRequest> Items { get; set; }
    }

}
