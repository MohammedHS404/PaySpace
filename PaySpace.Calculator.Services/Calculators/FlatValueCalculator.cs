using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;

namespace PaySpace.Calculator.Services.Calculators;

public class FlatValueCalculator : IFlatValueCalculator
{
    public decimal Calculate(decimal income, List<CalculatorSetting> settings)
    {
        decimal tax = 0;

        foreach (CalculatorSetting setting in settings)
        {
            if (setting.From > income)
            {
                continue;
            }

            if (setting.To.HasValue && setting.To.Value < income)
            {
                continue;
            }

            if (setting.RateType == RateType.Percentage)
            {
                tax = setting.Rate / 100 * income;
                break;
            }
            tax = setting.Rate;
            break;
        }

        return tax;
    }
}