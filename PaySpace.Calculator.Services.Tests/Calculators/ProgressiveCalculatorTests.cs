using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Calculators;
using Xunit;

namespace PaySpace.Calculator.Tests.Calculators;

public class ProgressiveCalculatorTests
{
    [Theory]
    [InlineData(32000, 4700)]
    [InlineData(15000, 750)]
    [InlineData(10000, 0)]
    public void Test1(decimal income, decimal expected)
    {
        List<CalculatorSetting> settings = new()
        {
            new()
            {
                Id = 1,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 0,
                From = 0,
                To = 10000
            },
            new()
            {
                Id = 2,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 15,
                From = 10000,
                To = 20000
            },
            new()
            {
                Id = 3,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 25,
                From = 20000,
                To = 30000
            },
            new()
            {
                Id = 4,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 35,
                From = 30000,
                To = 35000
            },
        };

        ProgressiveCalculator calculator = new();

        decimal result = calculator.Calculate(income, settings);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(70000, 17000)]
    public void Test2(decimal income, decimal expected)
    {
        List<CalculatorSetting> settings = new()
        {
            new()
            {
                Id = 1,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 10,
                From = 0,
                To = 10000
            },
            new()
            {
                Id = 2,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 15,
                From = 10000,
                To = 20000
            },
            new()
            {
                Id = 3,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 20,
                From = 20000,
                To = 30000
            },
            new()
            {
                Id = 4,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 25,
                From = 30000,
                To = 40000
            },
            new()
            {
                Id = 5,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 30,
                From = 40000,
                To = 50000
            },
            new()
            {
                Id = 6,
                Calculator = CalculatorType.Progressive,
                RateType = RateType.Percentage,
                Rate = 35,
                From = 50000,
                To = null
            }
        };

        ProgressiveCalculator calculator = new();

        decimal result = calculator.Calculate(income, settings);

        Assert.Equal(expected, result);
    }

}