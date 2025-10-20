using System.Collections.Concurrent;

namespace SafeScribe.API.Services
{
    public class InMemoryTokenBlacklistService : ITokenBlacklistService
    {
        private readonly ConcurrentDictionary<string, bool> _blacklist = new();

        public Task AddToBlacklistAsync(string jti)
        {
            _blacklist.TryAdd(jti, true);
            return Task.CompletedTask;
        }

        public Task<bool> IsBlacklistedAsync(string jti)
        {
            return Task.FromResult(_blacklist.ContainsKey(jti));
        }
    }
}