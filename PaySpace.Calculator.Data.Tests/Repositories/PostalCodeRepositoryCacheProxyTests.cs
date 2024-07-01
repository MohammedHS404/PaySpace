using Moq;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Cache;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Data.Repositories.PostalCode;

namespace PaySpace.Calculator.Data.Tests.Repositories;

public class PostalCodeRepositoryCacheProxyTests
{
    [Fact]
    public async Task GetPostalCodesAsync_ShouldUseCorrectKeyAndTimeSpan()
    {
        // Arrange
        Mock<IPostalCodeRepository> postalCodeRepositoryMock = new();
        Mock<ICacheService> cacheServiceMock = new();

        string postalCode = "7441";

        CacheKeyBuilder cacheKeyBuilder = new();

        string key = cacheKeyBuilder.Add(nameof(PostalCodeRepository.GetCalculatorTypeByPostalCodeAsync)).Add(postalCode).Build();

        CalculatorType? expected = CalculatorType.FlatValue;

        cacheServiceMock.Setup(s => s.GetOrCreateAsync(key, It.IsAny<Func<Task<CalculatorType?>>>(), TimeSpan.FromDays(1)))
            .ReturnsAsync(expected);

        PostalCodeRepositoryCacheProxy cacheProxy = new(postalCodeRepositoryMock.Object, cacheServiceMock.Object, new CacheKeyBuilder());

        // Act

        CalculatorType? actual = await cacheProxy.GetCalculatorTypeByPostalCodeAsync(postalCode, CancellationToken.None);

        // Assert

        Assert.Equal(expected, actual);

    }
}