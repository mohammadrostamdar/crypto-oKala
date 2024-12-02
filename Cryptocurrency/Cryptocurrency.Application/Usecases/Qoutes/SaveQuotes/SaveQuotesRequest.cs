using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.Usecases.Qoutes.SaveQuotes
{
    public class SaveQuotesRequest : IRequest<Unit>
    {
        public SaveQuotesRequest()
        {

        }
        public SaveQuotesRequest(string symbolName)
        {
            SymbolName = symbolName;
        }
        public string SymbolName { get; set; }
    }
}
