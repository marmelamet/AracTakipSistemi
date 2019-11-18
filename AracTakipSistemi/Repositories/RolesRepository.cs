using AracTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(AppIdentityDbContext _appIdentityDbContext) : base(_appIdentityDbContext)
        {
        }
    }
}
