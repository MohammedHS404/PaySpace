using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models;

public sealed class CalculatorHistory
{
    [Key]
    public long Id { get; set; }

    [MaxLength(50)]
    public required string PostalCode { get; set; }

    public DateTime Timestamp { get; set; }

    public decimal Income { get; set; }

    public decimal Tax { get; set; }

    public CalculatorType Calculator { get; set; }
    
    public static CalculatorHistory Create(string postalCode, decimal income, decimal tax, CalculatorType calculator)
    {
        return new()
        {
            PostalCode = postalCode,
            Timestamp = DateTime.UtcNow,
            Income = income,
            Tax = tax,
            Calculator = calculator
        };
    }
}