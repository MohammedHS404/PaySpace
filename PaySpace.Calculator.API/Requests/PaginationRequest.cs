using PaySpace.Calculator.Data.Dtos;

namespace PaySpace.Calculator.API.Requests;

public record PaginationRequest(int PageNumber = 1, int PageSize = 20)
{
    public PaginationDto ToPaginationDto()
    {
        return new PaginationDto(PageNumber, PageSize);
    }
}