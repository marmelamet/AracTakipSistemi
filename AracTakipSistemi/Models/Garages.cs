using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Models
{
    public class Garages
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public int Coord_X { get; set; }
        public int Coord_Y { get; set; }
        public string Convenience { get; set; }
    }
}
