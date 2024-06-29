using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Exceptions;
using Xunit;

namespace PaySpace.Calculator.Tests.Calculators;

public class CalculatorFactoryTests
{
    [Theory]
    [InlineData(CalculatorType.FlatRate, typeof(FlatRateCalculator))]
    [InlineData(CalculatorType.FlatValue, typeof(FlatValueCalculator))]
    [InlineData(CalculatorType.Progressive, typeof(ProgressiveCalculator))]
    public void CreateCalculator_ShouldReturnCorrectCalculator(CalculatorType type, Type expectedType)
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddTransient<IFlatRateCalculator>()
            .AddTransient<IFlatValueCalculator>()
            .AddTransient<IProgressiveCalculator>()
            .BuildServiceProvider();

        CalculatorFactory factory = new(serviceProvider);

        // Act
        ICalculator calculator = factory.CreateCalculator(type);

        // Assert
        Assert.IsType(expectedType, calculator);
    }
}

