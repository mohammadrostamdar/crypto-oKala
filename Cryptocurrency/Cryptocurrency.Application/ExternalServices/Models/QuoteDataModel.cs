using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.ExternalServices.Models
{
    public class QuoteDataModel
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("volume_24h")]
        public decimal Volume_24h { get; set; }
        [JsonProperty("volume_change_24h")]
        public decimal VolumeChange_24h { get; set; }
        [JsonProperty("percent_change_1h")]
        public decimal PercentChange_1h { get; set; }
        [JsonProperty("percent_change_24h")]
        public decimal PercentChange_24h { get; set; }
        [JsonProperty("percent_change_7d")]
        public decimal PercentChange_7d { get; set; }
        [JsonProperty("percent_change_30d")]
        public decimal PercentChange_30d { get; set; }
        [JsonProperty("market_cap")]
        public decimal MarketCap { get; set; }
        [JsonProperty("market_cap_dominance")]
        public decimal MarketCapDominance { get; set; }
        [JsonProperty("fully_diluted_market_cap")]
        public decimal FullyDilutedMarketCap { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
