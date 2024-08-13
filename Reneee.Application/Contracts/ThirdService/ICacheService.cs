namespace Reneee.Application.Contracts.ThirdService
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task RemoveAsync(string key);
        Task<bool> AcquireLockAsync(string key, TimeSpan expiration);
        Task ReleaseLockAsync(string key);
    }
}
