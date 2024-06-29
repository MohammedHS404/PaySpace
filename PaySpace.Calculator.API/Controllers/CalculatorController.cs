using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Requests;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public sealed class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private readonly IHistoryService _historyService;
    private readonly IMapper _mapper;
    private readonly ITaxCalculationService _taxCalculationService;
    private readonly IPostalCodeService _postalCodeService;

    public CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        IMapper mapper,
        ITaxCalculationService taxCalculationService,
        IPostalCodeService postalCodeService)
    {
        _logger = logger;
        _historyService = historyService;
        _mapper = mapper;
        _taxCalculationService = taxCalculationService;
        _postalCodeService = postalCodeService;
    }

    [HttpPost("calculate-tax")]
    public async Task<ActionResult<CalculateResult>> CalculateAsync([FromBody] CalculateRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        CalculatorType? calculatorType = await _postalCodeService.CalculatorTypeAsync(request.PostalCode, cancellationToken);

        if (!calculatorType.HasValue)
        {
            return BadRequest("Couldn't find an appropriate calculation method for the provided postal code");
        }

        try
        {
            CalculateTaxDto calculateTaxDto = new(request.PostalCode, calculatorType.Value, request.Income);

            CalculateResultDto result = await _taxCalculationService.CalculateTaxAsync(
                calculateTaxDto,
                cancellationToken);

            return Ok(result);
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

        return Ok(_mapper.Map<List<CalculatorHistoryDto>>(history));
    }
}