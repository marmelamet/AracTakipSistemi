using AracTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(AppIdentityDbContext _appIdentityDbContext) : base(_appIdentityDbContext)
        {
        }
    }
}
