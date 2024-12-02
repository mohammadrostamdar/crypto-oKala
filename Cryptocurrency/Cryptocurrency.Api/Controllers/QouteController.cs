using Cryptocurrency.Application.Configurations;
using Cryptocurrency.Application.Usecases.Qoutes.GetQuotes;
using Cryptocurrency.Application.Usecases.Qoutes.SaveQuotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocurrency.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : Controller
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly DefaultCryptoCurrencyConfiguration _defaultCryptoCurrencyConfiguration;
        private readonly IMediator _mediator;
        public QuoteController(
            ILogger<QuoteController> logger,
            DefaultCryptoCurrencyConfiguration defaultCryptoCurrencyConfiguration,
            IMediator mediator)
        {
            _logger = logger;
            _defaultCryptoCurrencyConfiguration = defaultCryptoCurrencyConfiguration;
            _mediator = mediator;
        }
        [HttpGet("")]
        public async Task<ActionResult> Get([FromQuery]GetQuoteRequest request)
        {
            //var data = JsonConvert.DeserializeObject<CryptocurrencyDataModel>(getvalue());
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                request = new GetQuoteRequest
                {
                    Name = _defaultCryptoCurrencyConfiguration.DefaultCryptocurrencyName
                };
            }
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
