using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Dtos;
using PaySpace.Calculator.Web.ViewModels;

namespace PaySpace.Calculator.Web.Controllers;

public class CalculatorController : Controller
{
    private readonly ICalculatorIntegrationService _calculatorIntegrationService;
    public CalculatorController(ICalculatorIntegrationService calculatorIntegrationService)
    {
        _calculatorIntegrationService = calculatorIntegrationService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken()]
    public async Task<IActionResult> Index(CalculatorViewModel request)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _calculatorIntegrationService.CalculateTaxAsync(new()
                {
                    PostalCode = request.PostalCode,
                    Income = request.Income
                });

                return RedirectToAction(nameof(History), new PaginationDto());
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        return View();
    }

    public async Task<IActionResult> History(
        [FromQuery] PaginationDto pagination,
        CancellationToken cancellationToken = default)
    {
        List<CalculatorHistoryDto> calculatorHistories = await _calculatorIntegrationService.GetHistoryAsync(new PaginationDto()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        }, cancellationToken);

        return View(new CalculatorHistoryViewModel
        {
            CalculatorHistory = calculatorHistories,
            Pagination = new()
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                HasNextPage = calculatorHistories.Count == pagination.PageSize,
                HasPreviousPage = pagination.PageNumber > 1
            }
        });
    }
}