namespace Cryptocurrency.Application.Configurations
{
    public class CoinMarketApiConfiguration
    {
        public string BaseUrl { get; set; }
        public int Timeout { get; set; }
        public string ApiKey { get; set; }
    }
}
