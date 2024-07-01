using PaySpace.Calculator.Data.Dtos;

namespace PaySpace.Calculator.Data.Abstractions;

public interface ICacheKeyBuilder
{
    ICacheKeyBuilder Add(string value);
    ICacheKeyBuilder Add(PaginationDto pagination);
    string Build();
}