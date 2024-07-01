using Microsoft.AspNetCore.Mvc.RazorPages;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Models;

public sealed class CalculatorHistoryViewModel : PageModel
{
    public required List<CalculatorHistory> CalculatorHistory { get; init; }
    public required PaginationViewModel Pagination { get; init; }

    
    
}