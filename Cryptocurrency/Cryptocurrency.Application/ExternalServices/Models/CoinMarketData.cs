using Newtonsoft.Json;

namespace Cryptocurrency.Application.ExternalServices.Models
{
    public class CoinMarketData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string SymbolName { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string,QuoteDataModel> Quote { get; set; }
    }
}
