using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Exceptions;
using Xunit;

namespace PaySpace.Calculator.Tests.Calculators;

public class FlatRateCalculatorTests
{
    [Theory]
    [InlineData(10000, 17.5, 1750)]
    [InlineData(10000, 15.5, 1550)]
    [InlineData(0, 10.5, 0)]
    [InlineData(10000, 0, 0)]
    public void Calculate_ShouldReturnCorrectTax(decimal income, decimal rate, decimal expectedTax)
    {
        // Arrange
        List<CalculatorSetting> settings = new()
        {
            new()
            {
                Rate = rate,
                RateType = RateType.Percentage,
                Calculator = CalculatorType.FlatRate
            }
        };

        FlatRateCalculator calculator = new();

        // Act
        decimal tax = calculator.Calculate(income, settings);

        // Assert
        Assert.Equal(expectedTax, tax);
    }

    [Fact]
    public void Calculate_ShouldThrowException_WhenRateTypeIsNotPercentage()
    {
        // Arrange
        List<CalculatorSetting> settings = new()
        {
            new()
            {
                Rate = 10,
                RateType = RateType.Amount,
                Calculator = CalculatorType.FlatRate
            }
        };

        FlatRateCalculator calculator = new();

        // Act & Assert
        Assert.Throws<DomainException>(() => calculator.Calculate(10000, settings));
    }

}