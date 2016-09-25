using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alb.Database;
using Dapper;

namespace Alb.Models
{
    public class AlbumRepository : IResourceRepository<Album>
    {
        private IDbConnection _conn;
        public AlbumRepository(IConnection connection)
        {
            _conn = connection.Conn;
        }

        public int Create(Album album)
        {
            var sql = @"INSERT INTO albums (title) VALUES (@Title) RETURNING id";
            var id = _conn.Query<int>(sql, new { Title = album.Title }).First();
            return id;
        }

        public void Delete(int id)
        {
            _conn.Execute(@"DELETE FROM albums WHERE id = @Id", new { Id = id });
        }

        public Album Find(int id)
        {
            return _conn.Query<Album>("SELECT * FROM albums WHERE id = @AlbumId", new { AlbumID = id }).FirstOrDefault();
        }

        public virtual IEnumerable<Album> FindAll(int id)
        {
            return _conn.Query<Album>("SELECT * FROM albums");
        }

        public Album Update(int id, Album album)
        {
            _conn.Execute(@"UPDATE albums SET title = @Title WHERE id = @Id", new { Title = album.Title, Id = id });
            return Find(id);
        }
    }
}