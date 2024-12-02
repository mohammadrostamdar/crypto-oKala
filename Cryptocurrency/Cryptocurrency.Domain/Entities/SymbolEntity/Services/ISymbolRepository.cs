using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Domain.Entities.SymbolEntity.Services;

public interface ISymbolRepository
{
    public Task Save(Symbol symbol);
}
