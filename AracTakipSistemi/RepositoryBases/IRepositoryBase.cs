using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
