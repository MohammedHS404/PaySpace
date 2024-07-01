using Microsoft.EntityFrameworkCore.Migrations;
using Moq;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Cache;
using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Data.Repositories.CalculatorSettings;
using PaySpace.Calculator.Data.Repositories.History;

namespace PaySpace.Calculator.Data.Tests.Repositories;

public class CalculatorSettingsRepositoryCacheProxyTests
{
    [Fact]
    public async Task GetSettingsAsync_ShouldUseCorrectKeyAndTimeSpan()
    {
        Mock<ICalculatorSettingsRepository> calculatorHistoryRepositoryMock = new();
        Mock<ICacheService> cacheServiceMock = new();

        CalculatorType calculatorType = CalculatorType.FlatValue;

        CacheKeyBuilder cacheKeyBuilder = new();

        string key = cacheKeyBuilder.Add(nameof(CalculatorSetting)).Add(calculatorType.ToString()).Build();

        List<CalculatorSetting> expected = new();

        cacheServiceMock.Setup(s => s.GetOrCreateAsync(key, It.IsAny<Func<Task<List<CalculatorSetting>>>>(), TimeSpan.FromDays(1)))
            .ReturnsAsync(expected);

        CalculatorSettingsRepositoryCacheProxy cacheProxy = new(calculatorHistoryRepositoryMock.Object, cacheServiceMock.Object, new CacheKeyBuilder());

        List<CalculatorSetting> acutal = await cacheProxy.GetSettingsAsync(calculatorType, CancellationToken.None);

        Assert.Equal(expected, acutal);
    }
}