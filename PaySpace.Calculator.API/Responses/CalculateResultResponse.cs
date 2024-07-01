using PaySpace.Calculator.Business.Dtos;

namespace PaySpace.Calculator.API.Responses;

public record CalculateResultResponse
{
    public string Calculator { get; init; }

    public decimal Tax { get; init; }

    public CalculateResultResponse(string calculator, decimal tax)
    {
        Calculator = calculator;
        Tax = tax;
    }

    public CalculateResultResponse(CalculateTaxResultDto result)
    {
        Calculator = result.Calculator.ToString();
        Tax = result.Tax;
    }
}