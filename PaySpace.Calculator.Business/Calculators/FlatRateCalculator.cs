using PaySpace.Calculator.Business.Abstractions.Calculators;
using PaySpace.Calculator.Business.Exceptions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Calculators;

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