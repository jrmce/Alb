using System.Data;
using Alb.Database;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Linq;
using System;

namespace Alb.Models.Repositories
{
    public class ShareRepository : IShareRepository
    {
        private IDbConnection _conn;
        private readonly ILogger<Connection> _logger;
        public ShareRepository(IConnection connection, ILogger<Connection> logger)
        {
            _logger = logger;
            _conn = connection.Conn;
        }

        public Share Find(int id)
        {
            var sql = @"
                SELECT * 
                FROM shares 
                WHERE id = @Id";
    
            return _conn.Query<Share>(sql, new { Id = id }).FirstOrDefault();
        }

        public Share FindByToken(Guid token)
        {
            var sql = @"
                SELECT * 
                FROM shares 
                WHERE token = @Token";
    
            return _conn.Query<Share>(sql, new { Token = token }).FirstOrDefault();
        }

        public int Create(CreateShare share)
        {
            var sql = @"
                INSERT INTO shares (name, token, expired_at, created_id)
                VALUES (@Name, @Token, @ExpiredAt, @CreatedAt)
                RETURNING id";

            var id = _conn.Query<int>(sql, new {
                Name = share.Name,
                Token = Guid.NewGuid(),
                ExpiredAt = share.ExpiredAt,
                CreatedAt = DateTime.Now
            }).First();
            
            return id;
        }
    }
}