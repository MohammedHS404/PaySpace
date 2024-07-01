using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Abstractions;

public interface IPostalCodeRepository
{
    Task<CalculatorType?> GetCalculatorTypeByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);
}