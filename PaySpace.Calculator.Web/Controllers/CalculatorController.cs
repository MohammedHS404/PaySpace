using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers;

public class CalculatorController : Controller
{
    private readonly ICalculatorHttpService _calculatorHttpService;
    public CalculatorController(ICalculatorHttpService calculatorHttpService)
    {
        _calculatorHttpService = calculatorHttpService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken()]
    public async Task<IActionResult> Index(CalculateRequestViewModel request)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _calculatorHttpService.CalculateTaxAsync(new()
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
        List<CalculatorHistory> calculatorHistories = await _calculatorHttpService.GetHistoryAsync(new PaginationDto()
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