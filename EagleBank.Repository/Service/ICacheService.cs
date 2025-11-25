namespace EagleBank.Repository.Service
{
    public interface ICacheService
    {
        Task WriteToCacheAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<(bool found, T value)> GetFromCacheAsync<T>(string key);
        Task<bool> RemoveFromCacheAsync(string key);
    }
}
