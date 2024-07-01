using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Abstractions;

public interface IPostalCodeService
{
    Task<CalculatorType?> GetCalculatorTypeByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);
}