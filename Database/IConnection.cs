using System.Data;

namespace Alb.Database
{
    public interface IConnection
    {
        IDbConnection Conn { get; }
    }
}