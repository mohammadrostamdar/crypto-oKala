using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.Dtos
{
    public class SymbolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SymbolName { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
