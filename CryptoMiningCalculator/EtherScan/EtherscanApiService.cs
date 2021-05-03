using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoMiningCalculator.EtherScan
{
    public static class EtherscanApiService
    {
        private static decimal ETH_VALUE_DIVIDER = 1000000000000000000;

        public static async Task<decimal> GetWalletValue(HttpClient client, string walletId, string etherScanApiKey)
        {
            var response = await client.GetAsync($"https://api.etherscan.io/api?module=account&action=balance&address={walletId}&tag=latest&apikey={etherScanApiKey}");
            var content = await response.Content.ReadAsStringAsync();
            var etherscanResponse = JsonSerializer.Deserialize<WalletDto>(content);

#if DEBUG
            Console.WriteLine("Got response from EtherScan API");
#endif

            return decimal.Parse(etherscanResponse.Result) / ETH_VALUE_DIVIDER;
        }
    }
}