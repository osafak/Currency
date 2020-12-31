using AutoMapper;
using Currency.External;
using Currency.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currency.Core.Exception;
namespace Currency.Service
{
    public interface ICurrencyService
    {
        Task<ResponseModel<List<Model.CurrencyModel>>> List(CurrencyListRequestModel model);
    }
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyHttpClient _currencyHttpClient;
        private readonly IMapper _mapper;


        public CurrencyService(ICurrencyHttpClient currencyHttpClient, IMapper mapper)
        {
            _currencyHttpClient = currencyHttpClient;
            _mapper = mapper;

        }
        public async Task<ResponseModel<List<Model.CurrencyModel>>> List(CurrencyListRequestModel request)
        {
            var listOfCurrency = await _currencyHttpClient.GetCurrenciesAsync();

            var data = _mapper.Map<List<Model.CurrencyModel>>(listOfCurrency);
            var query = data.AsQueryable();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(request.Name.ToLower())).AsQueryable();
            }

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                switch (request.OrderBy.ToLower())
                {
                    case "asc":
                        query = query.OrderBy(q => q.ForexBuying);
                        break;
                    case "desc":
                        query = query.OrderByDescending(q => q.ForexBuying);
                        break;
                }
            }

            var result = query.ToList();
            return new ResponseModel<List<Model.CurrencyModel>>
            {
                Data = result,
                TotalCount = result.Count
            };
        }
    }
}
