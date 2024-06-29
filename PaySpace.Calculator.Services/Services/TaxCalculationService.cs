using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Services;

public class TaxCalculationService : ITaxCalculationService
{
    private readonly ICalculatorSettingsRepository _calculatorSettingsRepository;
    private readonly ICalculatorFactory _calculatorFactory;
    private readonly IHistoryService _historyService;

    public TaxCalculationService(
        ICalculatorSettingsRepository calculatorSettingsRepository,
        ICalculatorFactory calculatorFactory,
        IHistoryService historyService)
    {
        _calculatorSettingsRepository = calculatorSettingsRepository;
        _calculatorFactory = calculatorFactory;
        _historyService = historyService;

    }

    public async Task<CalculateResultDto> CalculateTaxAsync(
        CalculateTaxDto calculateTaxDto,
        CancellationToken cancellationToken)
    {
        List<CalculatorSetting> calculatorSettings = await _calculatorSettingsRepository.GetSettingsAsync(calculateTaxDto.CalculatorType, cancellationToken);

        if (!calculatorSettings.Any())
        {
            throw new DomainException("Couldn't find an appropriate calculation for the provided postal code");
        }

        ICalculator calculator = _calculatorFactory.CreateCalculator(calculateTaxDto.CalculatorType);

        decimal tax = calculator.Calculate(calculateTaxDto.Income, calculatorSettings);

        CalculatorHistory history = CalculatorHistory.Create(calculateTaxDto.PostalCode, calculateTaxDto.Income, tax, calculateTaxDto.CalculatorType);

        await _historyService.AddAndSaveAsync(history, cancellationToken);

        return new(calculateTaxDto.CalculatorType, tax);
    }
}