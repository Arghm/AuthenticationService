using Microsoft.Extensions.Caching.Memory;

namespace AuthenticationService.Api.Application.Services.Cache.MemoryCache
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Set<T>(string key, T item)
        {
            return _memoryCache.Set(key, item);
        }

        public T Set<T>(string key, T item, MemoryCacheEntryOptions options)
        {
            return _memoryCache.Set(key, item, options);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public bool TryGetValue(string key, out object obj)
        {
            return _memoryCache.TryGetValue(key, out obj);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
