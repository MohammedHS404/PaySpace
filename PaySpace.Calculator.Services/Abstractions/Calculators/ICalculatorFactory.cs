using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Calculators;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator(CalculatorType type);
}