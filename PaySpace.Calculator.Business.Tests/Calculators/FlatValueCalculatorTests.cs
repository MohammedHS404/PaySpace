using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Business.Calculators;
using Xunit;

namespace PaySpace.Calculator.Tests.Calculators;

public class FlatValueTests
{
    [Theory]
    [InlineData(100000, 5000)]
    [InlineData(200000, 10000)]
    public void DefaultCase(decimal income, decimal expected)
    {
        List<CalculatorSetting> settings = new()
        {
            new()
            {
                Id = 7,
                Calculator = CalculatorType.FlatValue,
                RateType = RateType.Percentage,
                Rate = 5,
                From = 0,
                To = 199999
            },
            new()
            {
                Id = 8,
                Calculator = CalculatorType.FlatValue,
                RateType = RateType.Amount,
                Rate = 10000,
                From = 200000,
                To = null
            },
        };
        
        FlatValueCalculator calculator = new();
        
        decimal result = calculator.Calculate(income, settings);
        
        Assert.Equal(expected, result);
    }
}