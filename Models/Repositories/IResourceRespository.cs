using System.Collections.Generic;

namespace Alb.Models.Repositories
{
    public interface IResourceRepository<T>
    {
        IEnumerable<T> FindAll();
        T Find(int id);
        int Create(T resource);
        void Delete(int id);
        T Update(int id, T resource);
    }
}