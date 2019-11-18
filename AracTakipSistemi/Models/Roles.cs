using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Models
{
    public class Roles:IdentityRole
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
}
