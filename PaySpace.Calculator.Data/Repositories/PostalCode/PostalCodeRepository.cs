using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories.PostalCode;

public class PostalCodeRepository : IPostalCodeRepository
{
    private readonly CalculatorContext _context;

    public PostalCodeRepository(CalculatorContext context)
    {
        _context = context;
    }

    public async Task<CalculatorType?> GetCalculatorTypeByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
    {
        Models.PostalCode? code= await _context.PostalCodes
            .Where(x => x.Code == postalCode)
            .FirstOrDefaultAsync(cancellationToken);

        if (code == null)
        {
            return null;
        }
        
        return code.Calculator;
    }
}