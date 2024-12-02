using Cryptocurrency.Domain.Entities.SymbolEntity;
using Cryptocurrency.Domain.Entities.SymbolEntity.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Cryptocurrency.Infrastructure.Repositories.SymbolRepositories
{
    public class SymbolRepository : ISymbolRepository
    {
        private readonly IMemoryCache _memoryCache;
        public SymbolRepository(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task Save(Symbol symbol)
        {
            if(_memoryCache.TryGetValue(symbol.SymbolName, out var storedSymbol))
                _memoryCache.Remove(symbol.Id);
            _memoryCache.Set(symbol.SymbolName, symbol);
        }
    }
}
