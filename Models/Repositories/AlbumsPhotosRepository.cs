using System;
using System.Collections.Generic;
using System.Data;
using Alb.Database;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Linq;

namespace Alb.Models.Repositories
{
    public class AlbumsPhotosRepository : IAlbumsPhotosRepository
    {
        private IDbConnection _conn;
        private readonly ILogger<Connection> _logger;
        public AlbumsPhotosRepository(IConnection connection, ILogger<Connection> logger)
        {
            _logger = logger;
            _conn = connection.Conn;
        }

        public int Create(AlbumsPhotos albumsPhotos)
        {
            var sql = @"
                INSERT INTO albums_photos (album_id, photo_id)
                VALUES (@AlbumId, @PhotoId)
                RETURNING id";

            var id = _conn.Query<int>(sql, new { AlbumId = albumsPhotos.AlbumId, PhotoId = albumsPhotos.PhotoId }).First();
            return id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AlbumsPhotos Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlbumsPhotos> FindAll()
        {
            throw new NotImplementedException();
        }

        public AlbumsPhotos Update(int id, AlbumsPhotos albumsPhotos)
        {
            throw new NotImplementedException();
        }
    }
}