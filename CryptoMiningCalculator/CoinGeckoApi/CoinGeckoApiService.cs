using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoMiningCalculator.CoinGeckoApi
{
    public static class CoinGeckoApiService
    {
        public static async Task<CoinGeckoResponse> GetData(HttpClient client, string currencyISO)
        {
            var response = await client.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies={currencyISO}");
            var content = await response.Content.ReadAsStringAsync();
            var coinGeckoResponse = JsonSerializer.Deserialize<CoinGeckoResponse>(content);

#if DEBUG
            Console.WriteLine("Got response from CoinGecko API");
#endif

            return coinGeckoResponse;
        }
    }
}