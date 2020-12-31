using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Model
{
    public class CurrencyModel
    {
		public string Unit { get; set; }
		public string Name { get; set; }
		public string CurrencyName { get; set; }
		public string ForexBuying { get; set; }
		public string ForexSelling { get; set; }
		public string BanknoteBuying { get; set; }
		public string BanknoteSelling { get; set; }
		public string CrossRateUSD { get; set; }
		public string CrossRateOther { get; set; }
		public string CrossOrder { get; set; }
		public string Code { get; set; }
		public string CurrencyCode { get; set; }
	}
}
