using System.Collections.Concurrent;

namespace EagleBank.Repository.Service
{
    public class CacheService(TimeSpan? expirationTime) : ICacheService
    {
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new();
        private readonly TimeSpan DefaultExpiration = expirationTime ?? TimeSpan.FromMinutes(5);
        private class CacheItem
        {
            public required object Value { get; set; }
            public DateTime? Expiration { get; set; }
        }
        public Task<(bool found, T value)> GetFromCacheAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                if (item.Expiration == null || item.Expiration > DateTime.UtcNow)
                {
                    return Task.FromResult((true, (T)item.Value));
                }
                _cache.TryRemove(key, out _);
            }

            return Task.FromResult((false, default(T)));
        }

        public Task WriteToCacheAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var item = new CacheItem
            {
                Value = value,
                Expiration = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : DateTime.UtcNow.Add(DefaultExpiration)
            };

            _cache.AddOrUpdate(key, item, (k, v) => item);
            return Task.CompletedTask;
        }

        public Task<bool> RemoveFromCacheAsync(string key)
        {
            return Task.FromResult(_cache.TryRemove(key, out _));
        }
    }
}
