using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CardiacPatientMonitoringSystem.Services;

public class CacheVersionService : ICacheVersionService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<CacheVersionService> _logger;

    public CacheVersionService(
        IConnectionMultiplexer redis,
        ILogger<CacheVersionService> logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task<string> GetVersionAsync(string resource)
    {
        var key = $"{resource}:version";

        var database = _redis.GetDatabase();

        var version = await database.StringGetAsync(key);

        if (!version.HasValue)
        {
            await database.StringSetAsync(
                key,
                "1",
                when: When.NotExists);

            version = await database.StringGetAsync(key);
        }

        return version.ToString();
    }

    public async Task InvalidateAsync(string resource)
    {
        var key = $"{resource}:version";

        var database = _redis.GetDatabase();

        var newVersion = await database.StringIncrementAsync(key);

        _logger.LogInformation(
            "Cache version for {Resource} incremented to {Version}",
            resource,
            newVersion);
    }
}