using Microsoft.AspNetCore.Mvc.RazorPages;
using PaySpace.Calculator.Web.Services.Dtos;

namespace PaySpace.Calculator.Web.ViewModels;

public sealed class CalculatorHistoryViewModel : PageModel
{
    public required List<CalculatorHistoryDto> CalculatorHistory { get; init; }
    public required PaginationViewModel Pagination { get; init; }

    
    
}