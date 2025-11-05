namespace SmartWarehouseAPI.Models
{
    public class RutaEntrega
    {
        public int IdRuta { get; set; }
        public int IdRepartidor { get; set; }
        public DateTime FechaRuta { get; set; }
        public decimal? DistanciaEstimadaKm { get; set; }
        public int? DuracionEstimadaMin { get; set; }
        public string Estado { get; set; }
    }
}
