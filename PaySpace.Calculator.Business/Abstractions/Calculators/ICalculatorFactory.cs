using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Abstractions.Calculators;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator(CalculatorType type);
}