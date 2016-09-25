using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alb.Database;
using Dapper;

namespace Alb.Models
{
    public class PhotoRepository : IResourceRepository<Photo>
    {
        private IDbConnection _conn;
        public PhotoRepository(IConnection connection)
        {
            _conn = connection.Conn;
        }

        public int Create(Photo photo)
        {
            var sql = @"INSERT INTO photo (filename) VALUES (@Filename) RETURNING id";
            var id = _conn.Query<int>(sql, new { Title = photo.Filename }).First();
            return 1;
        }

        public void Delete(int id)
        {
            _conn.Execute(@"DELETE FROM photos WHERE id = @Id", new { Id = id });
        }

        public Photo Find(int id)
        {
            return _conn.Query<Photo>("SELECT * FROM photos WHERE id = @PhotoId", new { AlbumID = id }).FirstOrDefault();
        }

        public IEnumerable<Photo> FindAll(int albumId)
        {
            return _conn.Query<Photo>("SELECT * FROM photos WHERE album_id = @AlbumId", new { AlbumId = albumId });
        }

        public Photo Update(int id, Photo photo)
        {
            _conn.Execute(@"UPDATE photos SET filename = @Filename WHERE id = @Id", new { Filename = photo.Filename, Id = id });
            return Find(id);
        }
    }
}