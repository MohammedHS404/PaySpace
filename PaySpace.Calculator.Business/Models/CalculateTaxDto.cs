using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Models;

public record CalculateTaxDto
{
    public CalculateTaxDto(string PostalCode, CalculatorType CalculatorType, decimal Income)
    {
        this.PostalCode = PostalCode;
        this.CalculatorType = CalculatorType;
        this.Income = Income;
    }
    public string PostalCode { get; init; }
    public CalculatorType CalculatorType { get; init; }
    public decimal Income { get; init; }
    public void Deconstruct(out string PostalCode, out CalculatorType CalculatorType, out decimal Income)
    {
        PostalCode = this.PostalCode;
        CalculatorType = this.CalculatorType;
        Income = this.Income;
    }
}