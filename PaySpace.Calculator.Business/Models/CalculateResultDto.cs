using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Models;

public sealed record CalculateResultDto(CalculatorType Calculator, decimal Tax);
