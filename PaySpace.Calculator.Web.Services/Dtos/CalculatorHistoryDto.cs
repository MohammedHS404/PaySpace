namespace PaySpace.Calculator.Web.Services.Dtos;

public sealed class CalculatorHistoryDto
{
    public required string PostalCode { get; set; }

    public DateTime Timestamp { get; set; }

    public decimal Income { get; set; }

    public decimal Tax { get; set; }

    public required string Calculator { get; set; }
}