using PaySpace.Calculator.Data.Cache;

namespace PaySpace.Calculator.Data.Abstractions;

public interface ICacheKeyBuilder
{
    ICacheKeyBuilder Add(string value);
    string Build();
}