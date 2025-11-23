using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public int IdPedido { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public bool Activo { get; set; } = true;
    }
}
