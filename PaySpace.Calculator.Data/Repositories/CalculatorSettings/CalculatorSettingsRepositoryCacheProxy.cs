using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories.CalculatorSettings;

internal sealed class CalculatorSettingsRepositoryCacheProxy : ICalculatorSettingsRepository
{
    private readonly ICalculatorSettingsRepository _repository;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyBuilder _cacheKeyBuilder;

    public CalculatorSettingsRepositoryCacheProxy(
        ICalculatorSettingsRepository repository,
        ICacheService cacheService,
        ICacheKeyBuilder cacheKeyBuilder)
    {
        _repository = repository;
        _cacheService = cacheService;
        _cacheKeyBuilder = cacheKeyBuilder;

    }

    public async Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType, CancellationToken cancellationToken)
    {
        async Task<List<CalculatorSetting>> Factory()
        {
            return await _repository.GetSettingsAsync(calculatorType, cancellationToken);
        }

        string key = _cacheKeyBuilder.Add(nameof(CalculatorSetting)).Add(calculatorType.ToString()).Build();

        return await _cacheService.GetOrCreateAsync(key, Factory, TimeSpan.FromDays(1));
    }
}