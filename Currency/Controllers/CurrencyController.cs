using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Currency.External;
using Currency.Model;
using Currency.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Currency.Controllers
{
    [Route("api/")]
    [FormatFilter]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [Route("currency")]
        [HttpGet("{format?}")]
        public async Task<ActionResult> GetCurrencies([FromQuery] CurrencyListRequestModel request)
        {
            var response = await _currencyService.List(request);

            return Ok(response);
        }

        [Route("currency/csv")]
        public async Task<ActionResult> GetCurrenciesCSV([FromQuery] CurrencyListRequestModel request)
        {
            var response = await _currencyService.List(request);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Code,CurrencyName,CurrencyCode,BanknoteSelling,BanknoteBuying,CrossOrder,CrossRateOther,CrossRateUSD,ForexBuying,ForexSelling");
            foreach (var item in response.Data)
            {
                sb.AppendLine($"{item.Code},{item.CurrencyName},{item.CurrencyCode},{item.BanknoteSelling},{item.BanknoteBuying},{item.CrossOrder},{item.CrossRateOther},{item.CrossRateUSD},{item.ForexBuying},{item.ForexSelling}");
            }
            return File(System.Text.Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "data.csv");
        }
    }
}
