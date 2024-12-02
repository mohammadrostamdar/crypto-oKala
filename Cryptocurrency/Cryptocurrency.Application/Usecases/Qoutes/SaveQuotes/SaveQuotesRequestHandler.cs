using Cryptocurrency.Application.ExternalServices;
using Cryptocurrency.Domain.Entities.SymbolEntity;
using Cryptocurrency.Domain.Entities.SymbolEntity.DomainEvents.SymbolEvents;
using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using Cryptocurrency.Domain.Entities.SymbolEntity.Services;
using MediatR;

namespace Cryptocurrency.Application.Usecases.Qoutes.SaveQuotes
{
    public class SaveQuotesRequestHandler : IRequestHandler<SaveQuotesRequest, Unit>
    {
        private readonly ICryptocurrencyExternalService _cryptocurrencyExternalService;
        private readonly ISymbolRepository _symbolRepository;
        public SaveQuotesRequestHandler(
            ICryptocurrencyExternalService cryptocurrencyExternalService,
            ISymbolRepository symbolRepository)
        {
            _cryptocurrencyExternalService = cryptocurrencyExternalService;
            _symbolRepository = symbolRepository;
        }
        public async Task<Unit> Handle(SaveQuotesRequest request, CancellationToken cancellationToken)
        {
            var coinMarketResponse = await _cryptocurrencyExternalService.GetSymbolInfo(request.SymbolName);
            var firstRecord = coinMarketResponse.Data.FirstOrDefault().Value.FirstOrDefault();
            var domainEvent = new NewSymbolQuoteReceived();
            domainEvent.SymbolName = coinMarketResponse.Data.FirstOrDefault().Key;
            domainEvent.Name = firstRecord.Name;
            domainEvent.Id = firstRecord.Id;
            foreach (var quoteItem in firstRecord.Quote)
            {
                domainEvent.Quotes.Add(new Quote
                {
                    Name = quoteItem.Key,
                    FullyDilutedMarketCap = quoteItem.Value.FullyDilutedMarketCap,
                    LastUpdated = quoteItem.Value.LastUpdated,
                    MarketCap = quoteItem.Value.MarketCap,
                    MarketCapDominance = quoteItem.Value.MarketCapDominance,
                    PercentChange_1h = quoteItem.Value.PercentChange_1h,
                    PercentChange_24h = quoteItem.Value.PercentChange_24h,
                    PercentChange_30d = quoteItem.Value.PercentChange_30d,
                    PercentChange_7d = quoteItem.Value.PercentChange_7d,
                    Price = quoteItem.Value.Price,
                    VolumeChange_24h = quoteItem.Value.VolumeChange_24h,
                    Volume_24h = quoteItem.Value.Volume_24h
                });
            }
            var symbolInfo = new Symbol(domainEvent);
            await _symbolRepository.Save(symbolInfo);

            return Unit.Value;
        }
    }
}
