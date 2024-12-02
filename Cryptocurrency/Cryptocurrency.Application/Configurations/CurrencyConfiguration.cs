using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.Configurations
{
    public class CurrencyConfiguration
    {
        public CurrencyConfiguration()
        {
            Currencies = new List<string>();
        }
        public List<string> Currencies { get; set; }
    }
}
