using StackExchange.Redis;

namespace Ease.Data
{
    public class ConnectionHelper   
    {
        private static Lazy<ConnectionMultiplexer> _redisDbConnection;

        public static ConnectionMultiplexer RedisDbConnection
        {
            get
            {
                if (_redisDbConnection == null)
                {
                    _redisDbConnection = new Lazy<ConnectionMultiplexer>(() =>
                    {
                        return ConnectionMultiplexer.Connect("localhost:6379");
                    });
                }

                return _redisDbConnection.Value;
            }
        }

        public static ConnectionHelper()
        {
            _redisDbConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
            
        }

    }
}
