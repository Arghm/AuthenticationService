using Microsoft.Extensions.Caching.Memory;

namespace AuthenticationService.Api.Application.Services.Cache.MemoryCache
{
    public interface IMemoryCacheWrapper
    {
        public T Set<T>(string key, T item);
        T Set<T>(string key, T item, MemoryCacheEntryOptions options);
        bool TryGetValue<T>(string key, out T value);
        bool TryGetValue(string key, out object obj);
        void Remove(string key);
    }
}
