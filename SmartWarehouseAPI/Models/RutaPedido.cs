using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    public class RutaPedido
    {
        [Key]
        public int IdRuta { get; set; }
        public int IdPedido { get; set; }
    }
}
