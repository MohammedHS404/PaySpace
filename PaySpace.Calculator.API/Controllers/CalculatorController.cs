using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Requests;
using PaySpace.Calculator.API.Responses;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Business.Abstractions;
using PaySpace.Calculator.Business.Dtos;
using PaySpace.Calculator.Business.Exceptions;

namespace PaySpace.Calculator.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public sealed class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private readonly IHistoryService _historyService;
    private readonly ITaxCalculationService _taxCalculationService;
    private readonly IPostalCodeService _postalCodeService;

    public CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        ITaxCalculationService taxCalculationService,
        IPostalCodeService postalCodeService)
    {
        _logger = logger;
        _historyService = historyService;
        _taxCalculationService = taxCalculationService;
        _postalCodeService = postalCodeService;
    }

    [HttpPost("calculate-tax")]
    public async Task<ActionResult<CalculateTaxResultDto>> CalculateAsync([FromBody] CalculateTaxRequest taxRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        CalculatorType? calculatorType = await _postalCodeService.GetCalculatorTypeByPostalCodeAsync(taxRequest.PostalCode, cancellationToken);

        if (calculatorType == null)
        {
            return BadRequest("Couldn't find an appropriate calculation method for the provided postal code");
        }

        try
        {
            CalculateTaxDto calculateTaxDto = new(taxRequest.PostalCode, calculatorType.Value, taxRequest.Income);

            CalculateTaxResultDto taxResult = await _taxCalculationService.CalculateTaxAsync(
                calculateTaxDto,
                cancellationToken);

            return Ok(new CalculateResultResponse(taxResult));
        }
        catch (DomainException e)
        {
            _logger.LogError(e, e.Message);

            return StatusCode(500, "An error occurred while calculating the tax, please try again later");
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<CalculatorHistory>>> History([FromQuery] PaginationRequest? request, CancellationToken cancellationToken)
    {
        request ??= new PaginationRequest();

        List<CalculatorHistory> history = await _historyService.GetHistoryAsync(request.ToPaginationDto(), cancellationToken);

        List<CalculatorHistoryResponse> historyDtos = history.Select(CalculatorHistoryResponse.FromEntity).ToList();

        return Ok(historyDtos);
    }
}