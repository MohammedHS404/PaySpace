using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Dtos;

public record CalculateTaxDto
{
    public CalculateTaxDto(string postalCode, CalculatorType calculator, decimal income)
    {
        this.PostalCode = postalCode;
        this.Calculator = calculator;
        this.Income = income;
    }
    public string PostalCode { get; init; }
    public CalculatorType Calculator { get; init; }
    public decimal Income { get; init; }
}