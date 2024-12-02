using Cryptocurrency.Domain.Entities.SymbolEntity.DomainEvents.SymbolEvents;
using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cryptocurrency.Domain.Entities.SymbolEntity
{
    public class Symbol
    {
        public Symbol(NewSymbolQuoteReceived @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Quotes = @event.Quotes;
            SymbolName = @event.SymbolName;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string SymbolName { get; private set; }
        public List<Quote> Quotes { get; private set; }
    }
}
