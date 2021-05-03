using CryptoMiningCalculator.CoinGeckoApi;
using CryptoMiningCalculator.EtherScan;
using CryptoMiningCalculator.MinersApi;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoMiningCalculator
{
    internal class Program
    {
        private static readonly HttpClient _client = new();

        private static async Task Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Application created by Qtyb. Feel free to share and contribute :)\nGithub: https://github.com/Qtyb/CryptoMiningCalculator\n");

            if (args.Length < 2)
            {
                Console.WriteLine("Please enter a arguments: wallet id, currency ISO-4217 code (ex. USD) and optionally EtherScan Api Key for Wallet value check");
                return;
            }

            var walletId = args[0];
            var currencyISO = args[1];
            var etherScanApiKey = args[2];

            var minersResponse = await MinersApiService.GetData(_client, walletId);
            var coinGeckoResponse = await CoinGeckoApiService.GetData(_client, currencyISO);
            var conversionRate = coinGeckoResponse.currencyRate.PLN;

            decimal? walletValue = null;
            if (args.Length >= 3)
                walletValue = await EtherscanApiService.GetWalletValue(_client, walletId, etherScanApiKey);

            var status = new CryptoConverterStatus(walletId, currencyISO, "ETH", conversionRate, walletValue, minersResponse);
            DisplayStatusToConsole(status);
        }

        private static void DisplayStatusToConsole(CryptoConverterStatus cryptoConverterStatus)
        {
            Console.WriteLine();
            Console.WriteLine(cryptoConverterStatus.ToString());
            Console.WriteLine();

            Console.WriteLine("Conversion rates powered by CoinGecko API");
            Console.WriteLine("Wallet value powered by EtherScan API");
            Console.WriteLine("Mining data powered by 2Miners API");
        }
    }
}