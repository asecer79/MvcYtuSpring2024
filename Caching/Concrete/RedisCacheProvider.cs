using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caching.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace Caching.Concrete
{
    public class RedisCacheProvider:ICacheProvider
    {
        private readonly IDatabase _database = ConnectionMultiplexer.Connect("localhost:6379").GetDatabase(0);

        public bool Any(string key)
        {
            return _database.KeyExists(key);
        }

        public T? Get<T>(string key)
        {
            var result= _database.StringGet(key);

            var deserialize = JsonConvert.DeserializeObject<T>(result);

            return deserialize;
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var result = JsonConvert.SerializeObject(value);

           _database.StringSet(key, result, expiration);
        }

        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }
    }
}
