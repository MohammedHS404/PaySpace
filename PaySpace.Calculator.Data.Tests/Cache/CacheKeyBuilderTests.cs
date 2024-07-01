using PaySpace.Calculator.Data.Cache;
using PaySpace.Calculator.Data.Dtos;

namespace PaySpace.Calculator.Data.Tests.Cache;

public class CacheKeyBuilderTests
{
    [Fact]
    public void Add_ShouldAppendValueToKey()
    {
        // Arrange
        var cacheKeyBuilder = new CacheKeyBuilder();
        var value = "value";
        var value2 = "value2";

        // Act
        cacheKeyBuilder.Add(value);
        cacheKeyBuilder.Add(value2);
        
        string expected = $"{value}/{value2}";

        // Assert
        Assert.Equal(expected, cacheKeyBuilder.Build());
    }
    
    [Fact]
    public void Add_ShouldAppendPaginationToKey()
    {
        // Arrange
        var cacheKeyBuilder = new CacheKeyBuilder();
        string value = "value";
        var pagination = new PaginationDto
        {
            PageNumber = 1,
            PageSize = 10
        };

        cacheKeyBuilder.Add(value);
        // Act
        cacheKeyBuilder.Add(pagination);

        // Assert
        string expected = $"{value}/{pagination.PageNumber}_{pagination.PageSize}";
    }
}