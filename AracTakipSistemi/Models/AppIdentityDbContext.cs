using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AracTakipSistemi.Models;

namespace AracTakipSistemi.Models
{
    public class AppIdentityDbContext : IdentityDbContext<Users, Roles, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        public DbSet<AracTakipSistemi.Models.Expeditions> Expeditions { get; set; }
        public DbSet<AracTakipSistemi.Models.Garages> Garages { get; set; }
        public DbSet<AracTakipSistemi.Models.Roles> Roles { get; set; }
        public DbSet<AracTakipSistemi.Models.Vehicles> Vehicles { get; set; }
    }
}
