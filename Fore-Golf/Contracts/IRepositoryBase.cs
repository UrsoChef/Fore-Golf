using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> FindAll();
        Task<T> FindByID(Guid id);
        Task<bool> IsExists(Guid id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
