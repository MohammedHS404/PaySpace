using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models;

public sealed class PostalCode
{
    [Key]
    public long Id { get; set; }

    [MaxLength(50)]
    public required string Code { get; set; }

    public CalculatorType Calculator { get; set; }
}