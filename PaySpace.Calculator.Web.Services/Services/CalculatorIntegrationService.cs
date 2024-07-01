using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Dtos;

namespace PaySpace.Calculator.Web.Services.Services;

public class CalculatorIntegrationService : ICalculatorIntegrationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CalculatorIntegrationService> _logger;
    public CalculatorIntegrationService(HttpClient httpClient, CalculatorSettingsOptionsDto options, ILogger<CalculatorIntegrationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri($"{options.ApiUrl}/api/");
    }

    public async Task<List<CalculatorHistoryDto>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        string url = "calculator/history";
        url += $"?";
        url += $"PageNumber={pagination.PageNumber}";
        url += $"&";
        url += $"PageSize={pagination.PageSize}";

        Task<HttpResponseMessage> response = _httpClient.GetAsync(url, cancellationToken);

        if (response.Result.IsSuccessStatusCode)
        {
            List<CalculatorHistoryDto>? result = await response.Result.Content.ReadFromJsonAsync<List<CalculatorHistoryDto>>(cancellationToken: cancellationToken);

            if (result != null)
            {
                return result;
            }
            
            _logger.LogError("Failed to parse the response");
            
            throw new Exception("Failed to process the histories request");
        }
        
        string? message = await response.Result.Content.ReadAsStringAsync(cancellationToken);
        
        if (response.Result.StatusCode == HttpStatusCode.BadRequest)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new Exception(message);
            }
        }
        
        _logger.LogError(response.Result.ReasonPhrase);
        _logger.LogError(response.Result.StatusCode.ToString());
        
        throw new Exception("There was an issue processing histories request");
    }
    public async Task<CalculateTaxResultDto> CalculateTaxAsync(CalculateTaxRequestDto calculationTaxRequestDto)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("calculator/calculate-tax", calculationTaxRequestDto);

            if (response.IsSuccessStatusCode)
            {
                CalculateTaxResultDto? result = await response.Content.ReadFromJsonAsync<CalculateTaxResultDto>();

                if (result != null)
                {
                    return result;
                }

                _logger.LogError("Failed to parse the response");

                throw new Exception("Failed to process the tax calculation request");
            }

            string? message = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    throw new Exception(message);
                }
            }

            _logger.LogError(response.ReasonPhrase);
            _logger.LogError(response.StatusCode.ToString());

            throw new Exception("There was an issue processing your tax calculation request");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}