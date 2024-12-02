using Cryptocurrency.Application.ExternalServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.ExternalServices
{
    public interface ICryptocurrencyExternalService
    {
        public Task<CryptocurrencyDataModel> GetSymbolInfo(string symbolName); 
    }
}
