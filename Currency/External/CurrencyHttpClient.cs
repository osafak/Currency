using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Currency.Model;

namespace Currency.External
{

    public interface ICurrencyHttpClient
    {
        Task<List<Currency.Model.Currency>> GetCurrenciesAsync();
    }
    public class CurrencyHttpClient : ICurrencyHttpClient
    {

        private readonly HttpClient _httpClient;
        public CurrencyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Currency.Model.Currency>> GetCurrenciesAsync()
        {
            var xmlResponse = await _httpClient.GetAsync("kurlar/today.xml");
            var content = await xmlResponse.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Currency>), new XmlRootAttribute("Tarih_Date"));
            StringReader stringReader = new StringReader(content);
            var currencies = (List<Model.Currency>)serializer.Deserialize(stringReader);
            return currencies;
        }
    }
}
