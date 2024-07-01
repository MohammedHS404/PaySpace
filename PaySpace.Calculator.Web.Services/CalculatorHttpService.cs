using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services;

public class CalculatorHttpService : ICalculatorHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CalculatorHttpService> _logger;
    public CalculatorHttpService(HttpClient httpClient, CalculatorSettingsOptionsDto options, ILogger<CalculatorHttpService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri($"{options.ApiUrl}/api/");
    }

    public async Task<List<CalculatorHistory>> GetHistoryAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        string url = "calculator/history";
        url += $"?";
        url += $"PageNumber={pagination.PageNumber}";
        url += $"&";
        url += $"PageSize={pagination.PageSize}";

        Task<HttpResponseMessage> response = _httpClient.GetAsync(url, cancellationToken);

        if (response.Result.IsSuccessStatusCode)
        {
            List<CalculatorHistory>? result = await response.Result.Content.ReadFromJsonAsync<List<CalculatorHistory>>(cancellationToken: cancellationToken);

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Couldn't parse the response");
            }

        }
        else
        {
            throw new Exception(response.Result.ReasonPhrase);
        }
    }
    public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("calculator/calculate-tax", calculationRequest);

            if (response.IsSuccessStatusCode)
            {
                CalculateResult? result = await response.Content.ReadFromJsonAsync<CalculateResult>();

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