using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Business.Abstractions.Calculators;
using PaySpace.Calculator.Business.Exceptions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Calculators;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public CalculatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public ICalculator CreateCalculator(CalculatorType type)
    {
        return type switch
        {
            CalculatorType.Progressive => _serviceProvider.GetRequiredService<IProgressiveCalculator>(),
            CalculatorType.FlatValue => _serviceProvider.GetRequiredService<IFlatValueCalculator>(),
            CalculatorType.FlatRate => _serviceProvider.GetRequiredService<IFlatRateCalculator>(),
            _ => throw new DomainException("Couldn't find an appropriate calculation for the provided postal code")
        };
    }
}