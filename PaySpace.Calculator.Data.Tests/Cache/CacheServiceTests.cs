using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data.Cache;

namespace PaySpace.Calculator.Data.Tests.Cache;

public class CacheServiceTests
{
    [Fact]
    public async Task GetOrCreateAsync_WhenValueExists_ShouldReturnCachedValue()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cacheService = new CacheService(memoryCache);

        string key = "key";

        string expected = "value";

        memoryCache.Set(key, expected);

        // Act

        string actual = await cacheService.GetOrCreateAsync(key, () => Task.FromResult("new value"));

        // Assert

        Assert.Equal(expected, actual);

    }
    
    [Fact]
    public async Task GetOrCreateAsync_WhenValueDoesNotExists_ShouldReturnNewValue()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cacheService = new CacheService(memoryCache);

        string key = "key";

        string expected = "new value";

        // Act

        string actual = await cacheService.GetOrCreateAsync(key, () => Task.FromResult(expected));

        // Assert

        Assert.Equal(expected, actual);

    }
}