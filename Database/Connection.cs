using System.Data;
using Alb.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Alb.Database
{
    public class Connection : IConnection
    {
        private readonly DatabaseSettings _settings;
        private readonly ILogger<Connection> _logger;
        private IDbConnection _dbConn;
        public IDbConnection Conn
        {
            get
            {
                return _dbConn;
            }
        }

        public Connection(IOptions<DatabaseSettings> settings, ILogger<Connection> logger)
        {
            
            _logger = logger;
            _settings = settings.Value;
            _dbConn = new NpgsqlConnection(_settings.ConnectionString);
        }
    }
}