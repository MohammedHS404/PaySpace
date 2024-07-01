using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Services;

internal sealed class HistoryService : IHistoryService
{
    private readonly ICalculatorHistoryRepository _calculatorHistoryRepository;
    public HistoryService(ICalculatorHistoryRepository calculatorHistoryRepository)
    {
        _calculatorHistoryRepository = calculatorHistoryRepository;
    }
    public Task AddAndSaveAsync(CalculatorHistory history, CancellationToken cancellationToken)
    {
        _calculatorHistoryRepository.Add(history);
        return _calculatorHistoryRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<List<CalculatorHistory>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        return _calculatorHistoryRepository.GetHistoriesAsync(pagination, cancellationToken);
    }
}