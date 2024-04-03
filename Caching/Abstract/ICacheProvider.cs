namespace Caching.Abstract
{
    public interface ICacheProvider
    {
        bool Any(string key);
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);
        bool Remove(string key);
    }
}
