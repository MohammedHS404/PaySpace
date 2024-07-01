namespace PaySpace.Calculator.Web.Services.Dtos;

public sealed class CalculateTaxRequestDto
{
    public string? PostalCode { get; set; }

    public decimal Income { get; set; }
}