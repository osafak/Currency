using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Infrastructure
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Model.Currency, Model.CurrencyModel>();
        }
    }
}
