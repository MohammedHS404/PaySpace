using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Calculators;

public class Calculator
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
            

            if (setting.RateType == RateType.Percentage)
            {
                tax += setting.Rate / 100 * income;
            }
            else
            {
                tax += setting.Rate;
            }
        }

        return tax;
    }
}