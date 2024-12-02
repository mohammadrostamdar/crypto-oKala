using Cryptocurrency.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.Usecases.Qoutes.GetQuotes
{
    public class GetQuoteRequest : IRequest<SymbolDto>
    {
        public string Name { get; set; }
    }
}
