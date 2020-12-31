using Microsoft.VisualStudio.TestTools.UnitTesting;
using Currency.Service;
using Currency.Controllers;
using Currency.Model;
using Currency.External;
using System.Threading.Tasks;
using AutoMapper;
using Currency.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;

namespace CurrencyTest
{
    [TestFixture]
    public class CurrencyTest
    {
        
        List<Currency.Model.Currency> data = new List<Currency.Model.Currency>();
        [SetUp]
        public void Setup()
        {  
            data.Add(new Currency.Model.Currency
            {
                CurrencyName = "ABD DOLARI",
                Code = "USD",
                CurrencyCode = "USD",
                BanknoteBuying = "7.4011",
                BanknoteSelling = "7.4308",
                CrossOrder = "0",
                ForexBuying = "7.4063",
                ForexSelling = "7.4197"
            });
            data.Add(new Currency.Model.Currency
            {
                CurrencyName = "Danimarka Kronu",
                Code = "DKK",
                CurrencyCode = "DKK",
                BanknoteBuying = "5.4011",
                BanknoteSelling = "5.4308",
                CrossOrder = "0",
                ForexBuying = "5.4063",
                ForexSelling = "5.4197"
            });
            data.Add(new Currency.Model.Currency
            {
                CurrencyName = "EURO",
                Code = "EUR",
                CurrencyCode = "EUR",
                BanknoteBuying = "9.4011",
                BanknoteSelling = "9.4308",
                CrossOrder = "0",
                ForexBuying = "9.4063",
                ForexSelling = "9.4197"
            });
        }


        [Test]
        public async Task OrderByTest()
        {
            var mockHttpClient = new Moq.Mock<ICurrencyHttpClient>();
            mockHttpClient.Setup(q => q.GetCurrenciesAsync()).Returns(Task.FromResult(data));

            var myProfile = new Mapping();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new CurrencyService(mockHttpClient.Object, mapper);
            var result = await service.List(new CurrencyListRequestModel
            {
                OrderBy = "desc"
            });

            mockHttpClient.Verify(q => q.GetCurrenciesAsync());
            NUnit.Framework.Assert.AreEqual(result.Data[0].Code, "EUR");
        }

    }
}
