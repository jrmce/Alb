using System.Collections.Generic;

namespace Alb.Models.Repositories
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> FindAll();
        Photo Find(int id);
        int Create(CreatePhoto photo);
        void Delete(int id);
        Photo Update(int id, Photo photo);
    }
}