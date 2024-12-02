using Cryptocurrency.Application.Dtos;
using Cryptocurrency.Domain.Entities.SymbolEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.Extentions
{
    public static class SymbolConvertor
    {
        public static  SymbolDto ToSymbolDto(this Symbol model)
        {
            return new SymbolDto
            {
                Id = model.Id,
                Name = model.Name,
                Quotes = model.Quotes,
                SymbolName = model.SymbolName
            };
        }
    }
}
