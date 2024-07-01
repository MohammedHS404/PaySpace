using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Services;

public class PostalCodeService : IPostalCodeService
{
    private readonly IPostalCodeRepository _postalCodeRepository;

    public PostalCodeService(IPostalCodeRepository postalCodeRepository)
    {
        this._postalCodeRepository = postalCodeRepository;
    }

    public Task<CalculatorType?> GetCalculatorTypeByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
    {
        return _postalCodeRepository.GetCalculatorTypeByPostalCodeAsync(postalCode, cancellationToken);
    }
}