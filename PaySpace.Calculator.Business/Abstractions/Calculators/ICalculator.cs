using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Abstractions.Calculators;

public interface ICalculator
{
    decimal Calculate(decimal income, List<CalculatorSetting> settings);
}