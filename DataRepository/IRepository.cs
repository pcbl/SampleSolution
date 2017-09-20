using System.Collections.Generic;

namespace DataRepository
{
    public interface IRepository<out T>
    {
        IEnumerable<T> Get();
        
        T GetById(int id);
    }
}
