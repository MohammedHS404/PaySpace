using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Dtos;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories.History;

public class CalculatorHistoryRepository : ICalculatorHistoryRepository
{
    private readonly CalculatorContext _context;

    public CalculatorHistoryRepository(CalculatorContext context)
    {
        _context = context;
    }

    public void Add(CalculatorHistory history)
    {
        _context.Add(history);
    }

    public Task<List<CalculatorHistory>> GetHistoriesAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        return _context.Set<CalculatorHistory>()
            .OrderByDescending(cs => cs.Timestamp)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}