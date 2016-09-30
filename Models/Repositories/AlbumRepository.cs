using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alb.Database;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Alb.Models.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private IDbConnection _conn;
        private readonly ILogger<Connection> _logger;
        public AlbumRepository(IConnection connection, ILogger<Connection> logger)
        {
            _logger = logger;
            _conn = connection.Conn;
        }

        public int Create(CreateAlbum album)
        {
            var sql = @"
                INSERT INTO albums (title) 
                VALUES (@Title) 
                RETURNING id";

            var id = _conn.Query<int>(sql, new { Title = album.Title }).First();
            return id;
        }

        public void Delete(int id)
        {
            var sql = @"
                DELETE FROM albums 
                WHERE id = @Id";

            _conn.Execute(sql, new { Id = id });
        }

        public Album Find(int id)
        {
            var sql = @"
                SELECT * 
                FROM albums 
                WHERE id = @Id";

            var album = _conn.Query<Album>(sql, new { Id = id }).FirstOrDefault();
            album.Photos = FindAllPhotos(album.Id);
            return album;
        }

        public virtual IEnumerable<Album> FindAll()
        {
            var sql = @"
                SELECT * 
                FROM albums";

            var albums = _conn.Query<Album>(sql).ToList();
            albums.ForEach(a => a.Photos = FindAllPhotos(a.Id));
            return albums;
        }

        public Album Update(int id, Album album)
        {
            var sql = @"
                UPDATE albums 
                SET title = @Title 
                WHERE id = @Id";
                
            _conn.Execute(sql, new { Title = album.Title, Id = id });
            return Find(id);
        }

        public IEnumerable<int> FindAllPhotos(int id)
        {
            var sql = @"
                SELECT photos.id
                FROM photos
                JOIN albums_photos
                ON photos.id = albums_photos.photo_id 
                AND albums_photos.album_id = @AlbumId";

            return _conn.Query<int>(sql, new { AlbumId = id });
        }
    }
}