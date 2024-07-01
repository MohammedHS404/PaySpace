using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.API.Requests;

public sealed record CalculateTaxRequest
{
    [Required]
    [MinLength(1)]
    public string PostalCode { get; }

    [Required]
    [Range(0.001, double.MaxValue, ErrorMessage = "Income must be greater than 0")]
    public decimal Income { get; }
        
    public CalculateTaxRequest(string postalCode, decimal income)
    {
        PostalCode = postalCode;
        Income = income;
    }
}