using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Abstractions;

public interface IHistoryRepository
{
    void Add(CalculatorHistory history);
    Task<List<CalculatorHistory>> GetHistoriesAsync(PaginationDto pagination, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}