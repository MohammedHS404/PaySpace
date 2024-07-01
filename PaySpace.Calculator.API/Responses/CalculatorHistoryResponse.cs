using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.API.Responses;

public sealed record CalculatorHistoryResponse
{
    public required string PostalCode { get; init; }

    public DateTime Timestamp { get; init; }

    public decimal Income { get; init; }

    public decimal Tax { get; init; }

    public required string Calculator { get; init; }

    public static CalculatorHistoryResponse FromEntity(CalculatorHistory history)
    {
        return new()
        {
            PostalCode = history.PostalCode,
            Timestamp = history.Timestamp,
            Income = history.Income,
            Tax = history.Tax,
            Calculator = history.Calculator.ToString()
        };
    }
}