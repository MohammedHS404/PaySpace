using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services.Abstractions;

public interface ICalculatorHttpService
{
    Task<List<CalculatorHistory>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest);
}