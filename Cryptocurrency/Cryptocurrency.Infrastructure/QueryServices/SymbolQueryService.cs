using Cryptocurrency.Application.Dtos;
using Cryptocurrency.Application.Extentions;
using Cryptocurrency.Application.QueryServices;
using Cryptocurrency.Domain.Entities.SymbolEntity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Infrastructure.QueryServices
{
    public class SymbolQueryService : ISymbolQueryService
    {
        private readonly ILogger<SymbolQueryService> _logger;
        private readonly IMemoryCache _memoryCache;
        public SymbolQueryService(
            ILogger<SymbolQueryService> logger,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<SymbolDto> GetSymbolAsync(string name)
        {
            if (_memoryCache.TryGetValue(name,out var symbol))
            {
                var symbolDto = ((Symbol)symbol).ToSymbolDto();
                return symbolDto;
            }
            return null;
        }
    }
}
