using AracTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppIdentityDbContext AppIdentityDbContext { get; set; }

        public RepositoryBase(AppIdentityDbContext _appIdentityDbContext)
        {
            this.AppIdentityDbContext = _appIdentityDbContext;
        }

        public void Create(T entity)
        {
            this.AppIdentityDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.AppIdentityDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.AppIdentityDbContext.Set<T>().Remove(entity);
        }
    }
}
