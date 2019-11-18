using AracTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public class ExpeditionsRepository : RepositoryBase<Expeditions>, IExpeditionsRepository
    {
        public ExpeditionsRepository(AppIdentityDbContext _appIdentityDbContext) : base(_appIdentityDbContext)
        {
        }
    }
}
