using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface ITaxCalculationService
{
    Task<CalculateResultDto> CalculateTaxAsync(
        CalculateTaxDto calculateTaxDto,
        CancellationToken cancellationToken);
}