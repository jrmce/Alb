using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alb.Database;
using Dapper;

namespace Alb.Models.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private IDbConnection _conn;
        public PhotoRepository(IConnection connection)
        {
            _conn = connection.Conn;
        }

        public int Create(Photo photo)
        {
            var sql = @"
                INSERT INTO photos (filename) 
                VALUES (@Filename) 
                RETURNING id";

            var id = _conn.Query<int>(sql, new { Filename = photo.Filename }).First();
            return id;
        }

        public void Delete(int id)
        {
            var sql = @"
                DELETE FROM photos 
                WHERE id = @Id";

            _conn.Execute(sql, new { Id = id });
        }

        public Photo Find(int id)
        {
            var sql = @"
                SELECT * 
                FROM photos 
                WHERE id = @Id";
    
            return _conn.Query<Photo>(sql, new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Photo> FindAll()
        {
            var sql = @"
                SELECT * 
                FROM photos";

            return _conn.Query<Photo>(sql);
        }

        public Photo Update(int id, Photo photo)
        {
            var sql = @"
                UPDATE photos 
                SET filename = @Filename 
                WHERE id = @Id";

            _conn.Execute(sql, new { Filename = photo.Filename, Id = id });
            return Find(id);
        }
    }
}