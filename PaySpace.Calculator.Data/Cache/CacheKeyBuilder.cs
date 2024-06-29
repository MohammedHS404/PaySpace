using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Dtos;

namespace PaySpace.Calculator.Data.Cache;

internal sealed class CacheKeyBuilder : ICacheKeyBuilder
{
    private string _key = string.Empty;

    public ICacheKeyBuilder Add(string value)
    {
        _key += value;
        _key += "/";

        return this;
    }
    public ICacheKeyBuilder Add(PaginationDto pagination)
    {
        string key = $"{pagination.PageNumber}_{pagination.PageSize}";

        return Add(key);
    }

    public string Build()
    {
        if (_key.EndsWith("/"))
        {
            _key = _key.Remove(_key.Length - 1);
        }
        return _key;
    }
}