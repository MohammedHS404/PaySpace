using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Models;

public sealed record CalculateResultDto(CalculatorType Calculator, decimal Tax);
