using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service.Models
{
    public class PlantingDto
    {
        public int Square { get; set; }
        public int? FruitId { get; set; }
        public DateTime PlantingDate { get; set; }
        public decimal CoinBalance { get; set; }
    }
}
