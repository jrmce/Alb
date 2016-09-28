using System.Collections.Generic;

namespace Alb.Models.Repositories
{
    public interface IAlbumRepository : IResourceRepository<Album>
    {
        IEnumerable<int> FindAllPhotos(int id);
    }
}