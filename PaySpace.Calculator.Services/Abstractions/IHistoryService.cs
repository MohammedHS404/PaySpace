using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IHistoryService
{
    Task<List<CalculatorHistory>> GetHistoryAsync();

    Task AddAndSaveAsync(CalculatorHistory calculatorHistory, CancellationToken cancellationToken);

}