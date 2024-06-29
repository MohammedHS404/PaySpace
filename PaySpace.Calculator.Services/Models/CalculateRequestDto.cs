namespace PaySpace.Calculator.Services.Models;

public record CalculateRequestDto
{
    public CalculateRequestDto(string PostalCode, decimal Income)
    {
        this.PostalCode = PostalCode;
        this.Income = Income;
    }
    public string PostalCode { get; init; }
    public decimal Income { get; init; }
    public void Deconstruct(out string PostalCode, out decimal Income)
    {
        PostalCode = this.PostalCode;
        Income = this.Income;
    }
}