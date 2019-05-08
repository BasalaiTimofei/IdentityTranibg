using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityTraning.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(string id);
        void Create(T item);
        void Update(T item);
        Task Delete(string id);
    }
}
