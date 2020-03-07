using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string name);
        Task Create(T obj);
        Task<bool> Update(string id, T obj);
        Task<bool> Delete(string name); 
    }
}
