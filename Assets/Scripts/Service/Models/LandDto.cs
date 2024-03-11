using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service.Models
{
    public class LandDto
    {
        public int Level { get; set; }

        public List<PlantingDto> Plantings { get; set; }
    }
}
