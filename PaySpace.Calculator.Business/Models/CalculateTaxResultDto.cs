using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Models;

public sealed record CalculateTaxResultDto(CalculatorType Calculator, decimal Tax);
