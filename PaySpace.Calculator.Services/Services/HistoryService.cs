using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services.Services;

internal sealed class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _historyRepository;
    public HistoryService(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }
    public Task AddAndSaveAsync(CalculatorHistory history, CancellationToken cancellationToken)
    {
        _historyRepository.Add(history);
        return _historyRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<List<CalculatorHistory>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        return _historyRepository.GetHistoriesAsync(pagination, cancellationToken);
    }
}