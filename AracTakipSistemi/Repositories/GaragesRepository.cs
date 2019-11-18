using AracTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public class GaragesRepository : RepositoryBase<Garages>, IGaragesRepository
    {
        public GaragesRepository(AppIdentityDbContext _appIdentityDbContext) : base(_appIdentityDbContext)
        {
        }
    }
}
