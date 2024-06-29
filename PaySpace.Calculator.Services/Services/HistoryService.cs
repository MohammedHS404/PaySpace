using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services.Services;

internal sealed class HistoryService : IHistoryService
{
    private readonly CalculatorContext _context;
    public HistoryService(CalculatorContext context)
    {
        _context = context;
    }
    public Task AddAndSaveAsync(CalculatorHistory history, CancellationToken cancellationToken)
    {
        _context.Add(history);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<List<CalculatorHistory>> GetHistoryAsync()
    {
        return _context.Set<CalculatorHistory>()
            .OrderByDescending(_ => _.Timestamp)
            .ToListAsync();
    }
}