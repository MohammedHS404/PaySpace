namespace PaySpace.Calculator.Web.ViewModels;

public record PaginationViewModel
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public bool HasNextPage { get; init; } = true;
    public bool HasPreviousPage { get; init; }=false;
}