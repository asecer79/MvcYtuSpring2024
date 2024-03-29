using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caching.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Concrete
{
    public class MemoryCacheProvider(IMemoryCache memoryCache) : ICacheProvider
    {
        public bool Any(string key)
        {
            return memoryCache.Get(key) != null;
        }

        public T? Get<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            memoryCache.Set<T>(key, value, expiration);
        }

        public bool Remove(string key)
        { 
            memoryCache.Remove(key);
            return true;
        }
    }
}
