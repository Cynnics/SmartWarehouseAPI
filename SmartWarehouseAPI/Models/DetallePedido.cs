using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetalle { get; set; }

        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
