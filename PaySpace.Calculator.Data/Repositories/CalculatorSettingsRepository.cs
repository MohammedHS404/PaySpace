using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories;

internal sealed class CalculatorSettingsRepository : ICalculatorSettingsRepository
{
    private readonly CalculatorContext _context;
    
    public CalculatorSettingsRepository(CalculatorContext context)
    {
        _context = context;
    }
    
    public async Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType, CancellationToken cancellationToken)
    {
        return await _context.Set<CalculatorSetting>().AsNoTracking().Where(cs => cs.Calculator == calculatorType).ToListAsync(cancellationToken);
    }
}