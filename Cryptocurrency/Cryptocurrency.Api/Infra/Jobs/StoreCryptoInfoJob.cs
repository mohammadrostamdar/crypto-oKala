using Cryptocurrency.Application.Configurations;
using Cryptocurrency.Application.Usecases.Qoutes.SaveQuotes;
using MediatR;
using Quartz;

namespace Cryptocurrency.Api.Infra.Jobs
{
    public class StoreCryptoInfoJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StoreCryptoInfoJob> _logger;
        private readonly DefaultCryptoCurrencyConfiguration _defaultCryptoConfig;
        public StoreCryptoInfoJob(
            IMediator mediator, 
            ILogger<StoreCryptoInfoJob> logger,
            DefaultCryptoCurrencyConfiguration defaultCryptoConfig)
        {
            _mediator = mediator;
            _logger = logger;
            _defaultCryptoConfig = defaultCryptoConfig;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("JobStarted.");
            var request = new SaveQuotesRequest(_defaultCryptoConfig.DefaultCryptocurrencyName);
            await _mediator.Send(request);
        }
    }
}
