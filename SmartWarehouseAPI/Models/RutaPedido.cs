using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmartWarehouseAPI.Models
{
    [PrimaryKey(nameof(IdRuta), nameof(IdPedido))]
    public class RutaPedido
    {
        public int IdRuta { get; set; }

        public int IdPedido { get; set; }

    }
}
