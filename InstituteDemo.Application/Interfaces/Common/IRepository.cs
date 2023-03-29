using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstituteDemo.Application.Interfaces.Common
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(string include);
        Task<T> GetByIdAsync(int id);
    }
}
