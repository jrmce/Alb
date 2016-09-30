using System.Collections.Generic;

namespace Alb.Models.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> FindAll();
        Album Find(int id);
        int Create(CreateAlbum album);
        void Delete(int id);
        Album Update(int id, Album album);
        IEnumerable<int> FindAllPhotos(int id);
    }
}