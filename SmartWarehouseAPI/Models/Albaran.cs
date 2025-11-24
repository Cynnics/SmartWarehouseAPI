using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    public class Albaran
    {
        [Key]
        public int IdAlbaran { get; set; }
        public int IdPedido { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public int EntregadoPor { get; set; }
        public string RecibidoPor { get; set; }
        public string Estado { get; set; } = "generado";
    }
}
