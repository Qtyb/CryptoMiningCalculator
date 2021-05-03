using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoMiningCalculator.MinersApi
{
    public static class MinersApiService
    {
        public static async Task<MinersResponseDto> GetData(HttpClient client, string walletId)
        {
            var response = await client.GetAsync($"https://eth.2miners.com/api/accounts/{walletId}");
            var content = await response.Content.ReadAsStringAsync();
            var minersResponse = JsonSerializer.Deserialize<MinersResponseDto>(content);

#if DEBUG
            Console.WriteLine("Got response from 2Miners API");
#endif

            return minersResponse;
        }
    }
}