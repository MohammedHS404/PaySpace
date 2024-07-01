using PaySpace.Calculator.Web.Services.Dtos;

namespace PaySpace.Calculator.Web.Services.Abstractions;

public interface ICalculatorIntegrationService
{
    Task<List<CalculatorHistoryDto>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<CalculateTaxResultDto> CalculateTaxAsync(CalculateTaxRequestDto calculationTaxRequestDto);
}