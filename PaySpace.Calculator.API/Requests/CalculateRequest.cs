using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.API.Requests;

public sealed record CalculateRequest
{
    [Required]
    [MinLength(1)]
    public string PostalCode { get; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Income { get; }
        
    public CalculateRequest(string postalCode, decimal income)
    {
        PostalCode = postalCode;
        Income = income;
    }
}