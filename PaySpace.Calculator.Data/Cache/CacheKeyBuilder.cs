using PaySpace.Calculator.Data.Abstractions;

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
    
    public string Build()
    {
        if (_key.EndsWith("/"))
        {
            _key = _key.Remove(_key.Length - 1);
        }
        return _key;
    }
}