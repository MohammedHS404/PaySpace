namespace PaySpace.Calculator.Data.Dtos;

public class PaginationDto
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public PaginationDto()
    {
    }

    public PaginationDto(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}