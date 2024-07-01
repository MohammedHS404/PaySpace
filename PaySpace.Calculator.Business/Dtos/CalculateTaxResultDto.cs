using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Dtos;

public sealed record CalculateTaxResultDto(CalculatorType Calculator, decimal Tax);
