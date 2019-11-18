using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Models
{
    public class Vehicles
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public double Capacity { get; set; }
        public string MaintenanceSituation { get; set; }
    }
}
