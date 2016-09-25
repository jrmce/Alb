using System.Collections.Generic;

namespace Alb.Models
{
    public interface IResourceRepository<T>
    {
        IEnumerable<T> FindAll(int id = 0);
        T Find(int id);
        int Create(T resource);
        void Delete(int id);
        T Update(int id, T resource);
    }
}