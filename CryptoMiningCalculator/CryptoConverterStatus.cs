using CryptoMiningCalculator.MinersApi;
using System;
using System.Linq;

namespace CryptoMiningCalculator
{
    public class CryptoConverterStatus
    {
        private const int _ethValueDivider = 1000000000;

        public CryptoConverterStatus(
            string walletId,
            string currencyISO,
            string cryptoCode,
            decimal conversionRate,
            decimal? walletValue,
            MinersResponseDto minersResponseDto)
        {
            ConversionRate = conversionRate;
            CryptoCode = cryptoCode;
            WalletId = walletId;
            CurrencyISO = currencyISO;
            WalletValue = walletValue;
            MinersResponse = minersResponseDto;
        }

        private string WalletId { get; }
        private string CryptoCode { get; }
        private string CurrencyISO { get; }
        private decimal ConversionRate { get; }
        private MinersResponseDto MinersResponse { get; }

        public decimal? WalletValue { get; }
        public decimal UnpaidValue => (MinersResponse.Stats.Balance + MinersResponse.Stats.Immature) / _ethValueDivider;
        public decimal Last24h => MinersResponse.Last24hReward / _ethValueDivider;
        public decimal TotalPaidOut => MinersResponse.Payments.Sum(p => p.Amount) / _ethValueDivider;
        public decimal NextPayoutValue => 0.05M;

        public decimal GetLastHour()
        {
            var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var rewardsFromLastHour = MinersResponse.Rewards.Where(r => currentTimestamp - (60 * 60) <= r.Timestamp);
            return ((decimal)rewardsFromLastHour.Sum(r => r.Reward)) / _ethValueDivider;
        }

        public DateTime? GetNextPayoutDateTimeBasedLastHour()
        {
            if (GetLastHour() == 0)
                return null;

            var remainingHours = GetRemainingCoinsToPayout() / GetLastHour();
            return DateTime.Now.AddHours(decimal.ToDouble(remainingHours));
        }

        public DateTime? GetNextPayoutDateTimeBasedLast24Hours()
        {
            var meanHourlyGainFromLast24h = MinersResponse.Last24hReward / 24 / _ethValueDivider;
            if (meanHourlyGainFromLast24h == 0)
                return null;

            var remainingHours = GetRemainingCoinsToPayout() / meanHourlyGainFromLast24h;
            return DateTime.Now.AddHours(decimal.ToDouble(remainingHours));
        }

        public override string ToString()
        {
            return
                $"Wallet Id:      [{WalletId}]\n" +
                $"Currency:       [{CurrencyISO}]\n" +
                $"Conversion rate:[{ConversionRate}] {CurrencyISO}/{CryptoCode}\n" +
                $"Unpaid value:   [{UnpaidValue * ConversionRate}] {CurrencyISO}\n" +
                $"Last 24h:       [{Last24h * ConversionRate}] {CurrencyISO}\n" +
                $"Last hour:      [{GetLastHour() * ConversionRate}] {CurrencyISO}\n" +
                $"Paid out:       [{TotalPaidOut * ConversionRate}] {CurrencyISO}\n" +
                $"Next Payout:    [{GetNextPayoutDateTimeBasedLastHour() ?? DateTime.MaxValue}] (based on last hour)\n" +
                $"Next Payout:    [{GetNextPayoutDateTimeBasedLast24Hours() ?? DateTime.MaxValue}] (based on last 24 hours)\n" +
                $"Payout Value:   [{NextPayoutValue * ConversionRate}] {CurrencyISO}\n" +
                $"Wallet value:   [{WalletValue * ConversionRate}] {CurrencyISO}\n" +
                $"Unpaid value:   [{UnpaidValue}] {CryptoCode}\n" +
                $"Last 24h:       [{Last24h}] {CryptoCode}\n" +
                $"Last hour:      [{GetLastHour()}] {CryptoCode}\n" +
                $"Paid out:       [{TotalPaidOut}] {CryptoCode}\n" +
                $"Wallet value:   [{WalletValue}] {CryptoCode}";
        }

        private decimal GetRemainingCoinsToPayout()
        {
            return 0.05M - ((MinersResponse.Stats.Balance + MinersResponse.Stats.Immature) / _ethValueDivider);
        }
    }
}