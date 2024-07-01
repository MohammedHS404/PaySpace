using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Business.Abstractions.Calculators;
using PaySpace.Calculator.Business.Exceptions;
using PaySpace.Calculator.Business.Models;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Business.Services;

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

    public async Task<CalculateTaxResultDto> CalculateTaxAsync(
        CalculateTaxDto calculateTaxDto,
        CancellationToken cancellationToken)
    {
        List<CalculatorSetting> calculatorSettings = await _calculatorSettingsRepository.GetSettingsAsync(calculateTaxDto.Calculator, cancellationToken);

        if (!calculatorSettings.Any())
        {
            throw new DomainException("Couldn't find an appropriate calculation for the provided postal code");
        }

        ICalculator calculator = _calculatorFactory.CreateCalculator(calculateTaxDto.Calculator);

        decimal tax = calculator.Calculate(calculateTaxDto.Income, calculatorSettings);

        CalculatorHistory history = CalculatorHistory.Create(calculateTaxDto.PostalCode, calculateTaxDto.Income, tax, calculateTaxDto.Calculator);

        await _historyService.AddAndSaveAsync(history, cancellationToken);

        return new(calculateTaxDto.Calculator, tax);
    }
}