using _2MinersStats._2minersApi;
using _2MinersStats.CoinGeckoApi;
using _2MinersStats.EtherScan;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace _2MinersStats
{
    internal class Program
    {
        private const int minersEthValueDivider = 1000000000;
        private static readonly HttpClient _client = new();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Application created by Qtyb. Feel free to share :)\nGithub: https://github.com/Qtyb/CryptoMiningCalculator\n");

            if (args.Length < 2)
            {
                Console.WriteLine("Please enter a arguments: wallet id, currency ISO-4217 code (ex. USD) and optionally EtherScan Api Key for Wallet value check");
                return;
            }

            var walletId = args[0];
            var currencyISO = args[1];

            //GET CONVERSION VALUE
            var response2 = await _client.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies={currencyISO}");
            var content2 = await response2.Content.ReadAsStringAsync();
            var currencyRateResponse = JsonSerializer.Deserialize<CoinGeckoResponse>(content2).currencyRate;

            //GET 2MINERS API DATA
            var response = await _client.GetAsync($"https://eth.2miners.com/api/accounts/{walletId}");
            var content = await response.Content.ReadAsStringAsync();
            var minersResponse = JsonSerializer.Deserialize<_2MinersResponseDto>(content);

            //Wallet value
            decimal? walletValue = null;
            if (args.Length >= 3)
            {
                var etherScanApiKey = args[2];
                var response3 = await _client.GetAsync($"https://api.etherscan.io/api?module=account&action=balance&address={walletId}&tag=latest&apikey={etherScanApiKey}");
                var content3 = await response3.Content.ReadAsStringAsync();
                walletValue = decimal.Parse(JsonSerializer.Deserialize<WalletDto>(content3).Result) * currencyRateResponse.PLN;
            }

            //Calculate last hour gain
            var exampleValue = minersResponse.Rewards.Select(r => DateTimeOffset.UnixEpoch.AddSeconds(r.Timestamp)).First();
            var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var rewardsFromLastHour = minersResponse.Rewards.Where(r => currentTimestamp - (60 * 60) <= r.Timestamp);
            var rewardsValueFromLastHour = ((decimal)rewardsFromLastHour.Sum(r => r.Reward)) / minersEthValueDivider;

            //Calculate data
            var meanHourlyGainFromLast24h = minersResponse.Last24hReward / 24 / minersEthValueDivider;
            var remainingCurrencyToPayout = 0.05M - ((minersResponse.Stats.Balance + minersResponse.Stats.Immature) / minersEthValueDivider);

            var remainingHours = remainingCurrencyToPayout / meanHourlyGainFromLast24h;
            var nextPayoutDate = DateTime.Now.AddHours(decimal.ToDouble(remainingHours));
            var outputDto = new OutputDto
            {
                WalletId = walletId,
                Currency = currencyISO,
                UnPaidValue = (minersResponse.Stats.Balance + minersResponse.Stats.Immature) * currencyRateResponse.PLN / minersEthValueDivider,
                Last24hGain = minersResponse.Last24hReward * currencyRateResponse.PLN / minersEthValueDivider,
                PerHourGain = rewardsValueFromLastHour * currencyRateResponse.PLN,
                TotalPaidOut = minersResponse.TotalPaid * currencyRateResponse.PLN / minersEthValueDivider,
                NextPayoutDateTime = nextPayoutDate,
                NextPayoutValue = 0.05M * currencyRateResponse.PLN,
                WalletValue = walletValue
            };

            //Display data
            foreach (var p in outputDto.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
                Console.WriteLine($"{p.Name}: [{p.GetValue(outputDto, null)}]");

            Console.WriteLine();
            Console.WriteLine("Conversion rates powered by CoinGecko API");
            Console.WriteLine("Wallet value powered by EtherScan API");
            Console.WriteLine("Mining data powered by 2Miners API");
        }
    }
}