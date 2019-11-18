using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Models;

namespace AracTakipSistemi.Repositories
{
    public class VehiclesRepository : RepositoryBase<Vehicles>, IVehiclesRepository
    {
        public VehiclesRepository(AppIdentityDbContext _appIdentityDbContext) : base(_appIdentityDbContext)
        {
        }
    }
}
