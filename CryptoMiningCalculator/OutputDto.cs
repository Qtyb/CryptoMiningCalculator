using System;

namespace CryptoMiningCalculator
{
    public class OutputDto
    {
        public string WalletId { get; set; }
        public string Currency { get; set; }
        public decimal UnPaidValue { get; set; }
        public decimal Last24hGain { get; set; }
        public decimal PerHourGain { get; set; }
        public decimal TotalPaidOut { get; set; }
        public DateTime NextPayoutDateTime { get; set; }
        public decimal NextPayoutValue { get; set; }
        public decimal? WalletValue { get; set; }
    }
}