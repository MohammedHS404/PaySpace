using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services.Services;

internal sealed class PostalCodeService : IPostalCodeService
{
    private readonly CalculatorContext _context;
    private readonly IMemoryCache _memoryCache;
    public PostalCodeService(CalculatorContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    public Task<List<PostalCode>> GetPostalCodesAsync()
    {
        return _memoryCache.GetOrCreateAsync("PostalCodes", _ => _context.Set<PostalCode>().AsNoTracking().ToListAsync())!;
    }

    public async Task<CalculatorType?> CalculatorTypeAsync(string code, CancellationToken cancellationToken)
    {
        var postalCodes = await GetPostalCodesAsync();

        PostalCode? postalCode = postalCodes.FirstOrDefault(pc => pc.Code == code);

        return postalCode?.Calculator;
    }
}