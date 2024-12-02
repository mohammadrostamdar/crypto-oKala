using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Domain.Entities.SymbolEntity.Models
{
    public class Quote
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Volume_24h { get; set; }
        public decimal VolumeChange_24h { get; set; }
        public decimal PercentChange_1h { get; set; }
        public decimal PercentChange_24h { get; set; }
        public decimal PercentChange_7d { get; set; }
        public decimal PercentChange_30d { get; set; }
        public decimal MarketCap { get; set; }
        public decimal MarketCapDominance { get; set; }
        public decimal FullyDilutedMarketCap { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
