using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoMiningCalculator.CoinGeckoApi
{
    public class CoinGeckoResponse
    {
        [JsonPropertyName("ethereum")]
        public CurrencyRate currencyRate { get; set; }
    }

    public class CurrencyRate
    {
        [JsonPropertyName("pln")]
        public decimal PLN { get; set; }
    }
}
