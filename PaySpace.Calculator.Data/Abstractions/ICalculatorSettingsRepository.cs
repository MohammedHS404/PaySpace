using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Abstractions;

public interface ICalculatorSettingsRepository
{
    Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType, CancellationToken cancellationToken);
}