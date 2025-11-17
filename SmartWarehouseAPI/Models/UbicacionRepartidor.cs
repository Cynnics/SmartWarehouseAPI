using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    public class UbicacionRepartidor
    {
        [Key]
        public int IdUbicacion { get; set; }

        public int IdRepartidor { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
