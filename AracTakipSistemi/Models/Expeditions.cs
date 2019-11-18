using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Models
{
    public class Expeditions
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public int DepartGarageID { get; set; }
        public int ArrivalGarageID { get; set; }
        public int DriverID { get; set; }
        public int VehicleID { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
