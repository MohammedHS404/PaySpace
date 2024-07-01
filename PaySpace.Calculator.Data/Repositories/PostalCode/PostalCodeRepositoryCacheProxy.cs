using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories.PostalCode;

public class PostalCodeRepositoryCacheProxy : IPostalCodeRepository
{
    private readonly IPostalCodeRepository _postalCodeRepository;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyBuilder _cacheKeyBuilder;

    public PostalCodeRepositoryCacheProxy(IPostalCodeRepository postalCodeRepository, ICacheService cacheService, ICacheKeyBuilder cacheKeyBuilder)
    {
        _postalCodeRepository = postalCodeRepository;
        _cacheService = cacheService;
        _cacheKeyBuilder = cacheKeyBuilder;
    }

    public async Task<CalculatorType?> GetCalculatorTypeByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
    {
        Task<CalculatorType?> Factory()
        {
            return _postalCodeRepository.GetCalculatorTypeByPostalCodeAsync(postalCode, cancellationToken);
        }

        string key = _cacheKeyBuilder.Add(nameof(GetCalculatorTypeByPostalCodeAsync)).Add(postalCode).Build();

        return await _cacheService.GetOrCreateAsync(key, Factory, TimeSpan.FromDays(1));
    }
}