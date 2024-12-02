using Cryptocurrency.Application.Dtos;
using Cryptocurrency.Application.QueryServices;
using MediatR;

namespace Cryptocurrency.Application.Usecases.Qoutes.GetQuotes
{
    public class GetQuoteRequestHandler : IRequestHandler<GetQuoteRequest, SymbolDto>
    {
        private readonly ISymbolQueryService _symbolQueryService;
        public GetQuoteRequestHandler(
            ISymbolQueryService symbolQueryService)
        {
            _symbolQueryService = symbolQueryService;
        }
        public async Task<SymbolDto> Handle(GetQuoteRequest request, CancellationToken cancellationToken)
        {
            var result = await _symbolQueryService.GetSymbolAsync(request.Name);
            return result;
        }
    }
}
