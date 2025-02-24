using PaySpace.Calculator.Business.Abstractions.Calculators;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Calculators;

public class ProgressiveCalculator : IProgressiveCalculator
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

            decimal amount = 0;
            
            if (setting.To.HasValue && setting.To.Value < income)
            {
                amount = setting.To.Value - setting.From;
            }
            else
            {
                amount = income - setting.From;
            }

            if (setting.RateType == RateType.Percentage)
            {
                tax += setting.Rate / 100 * amount;
            }
            else
            {
                tax += setting.Rate;
            }
        }

        return tax;
    }
}