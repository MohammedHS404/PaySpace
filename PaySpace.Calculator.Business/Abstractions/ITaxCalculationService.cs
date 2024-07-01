using PaySpace.Calculator.Business.Models;

namespace PaySpace.Calculator.Business.Abstractions;

public interface ITaxCalculationService
{
    Task<CalculateResultDto> CalculateTaxAsync(
        CalculateTaxDto calculateTaxDto,
        CancellationToken cancellationToken);
}