using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.ExternalServices.Models
{
    public class CryptocurrencyDataModel
    {
        [JsonProperty("data")]
        public Dictionary<string, List<CoinMarketData>> Data { get; set; }

        [JsonProperty("status")]
        public JObject Status { get; set; }
    }
}
