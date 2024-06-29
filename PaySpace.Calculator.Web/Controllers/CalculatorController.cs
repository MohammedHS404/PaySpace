using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        var vm = GetCalculatorViewModelAsync();

        return View(vm);
    }

    public async Task<IActionResult> History()
    {
        return View(new CalculatorHistoryViewModel
        {
            CalculatorHistory = await _calculatorHttpService.GetHistoryAsync()
        });
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

                return RedirectToAction(nameof(History));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        CalculatorViewModel? vm = await GetCalculatorViewModelAsync(request);

        return View(vm);
    }

    private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
    {
        var postalCodes = await _calculatorHttpService.GetPostalCodesAsync();

        return new()
        {
            PostalCodes = new SelectList(postalCodes),
            Income = request.Income,
            PostalCode = request.PostalCode ?? string.Empty
        };
    }
}