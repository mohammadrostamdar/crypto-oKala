using AutoMapper;
using Cryptocurrency.Application.Dtos;
using Cryptocurrency.Application.ExternalServices.Models;
using Cryptocurrency.Domain.Entities.SymbolEntity;
using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Application.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Symbol, SymbolDto>()
                .ForMember(x => x.Id, s => s.MapFrom(p => p.Id))
                .ForMember(x => x.Name, s => s.MapFrom(p => p.Name))
                .ForMember(x => x.SymbolName, s => s.MapFrom(p => p.SymbolName))
                .ForMember(x => x.Quotes, s => s.MapFrom(p => p.Quotes));

            CreateMap<SymbolDto, Symbol>()
                .ForMember(x => x.Id, s => s.MapFrom(p => p.Id))
                .ForMember(x => x.Name, s => s.MapFrom(p => p.Name))
                .ForMember(x => x.SymbolName, s => s.MapFrom(p => p.SymbolName))
                .ForMember(x => x.Quotes, s => s.MapFrom(p => p.Quotes));

            CreateMap<Quote,QuoteDataModel>();

        }
    }
}
