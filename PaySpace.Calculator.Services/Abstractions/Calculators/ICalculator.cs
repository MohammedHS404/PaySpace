using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Calculators;

public interface ICalculator
{
    decimal Calculate(decimal income, List<CalculatorSetting> settings);
}