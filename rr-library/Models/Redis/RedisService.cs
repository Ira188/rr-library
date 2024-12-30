using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace rr_library.Models.Redis
{
    public interface IRedisService
    {
        IConnectionMultiplexer RedisConnection { get; }
        IDatabase RedisDatabase { get; }
    }
    public class RedisService : IRedisService
    {
        public IConnectionMultiplexer RedisConnection { get; private set; }
        public IDatabase RedisDatabase { get; private set; }
        public RedisService()
        {
            RedisConnection = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisConnection")!);
            RedisDatabase = RedisConnection.GetDatabase();
        }
    }
}
