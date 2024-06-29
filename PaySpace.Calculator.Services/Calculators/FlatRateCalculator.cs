using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.Services.Calculators;

public class FlatRateCalculator : IFlatRateCalculator
{
    public decimal Calculate(decimal income, List<CalculatorSetting> settings)
    {
        CalculatorSetting setting = settings.Single();

        if (setting.RateType != RateType.Percentage)
        {
            throw new DomainException("Rate type should be percentage");
        }

        return setting.Rate / 100 * income;
    }
}