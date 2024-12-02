using Cryptocurrency.Application.Dtos;

namespace Cryptocurrency.Application.QueryServices
{
    public interface ISymbolQueryService
    {
        public Task<SymbolDto> GetSymbolAsync(string name);
    }
}
