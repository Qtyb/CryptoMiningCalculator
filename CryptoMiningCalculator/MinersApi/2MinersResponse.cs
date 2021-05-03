using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoMiningCalculator.MinersApi
{
    public class MinersResponseDto
    {
        [JsonPropertyName("hashrate")]
        public decimal AverageHashRate { get; set; }

        [JsonPropertyName("24hreward")]
        public decimal Last24hReward { get; set; }

        [JsonPropertyName("payments")]
        public IEnumerable<PaymentDto> Payments { get; set; }

        [JsonPropertyName("stats")]
        public StatDto Stats { get; set; }

        [JsonPropertyName("rewards")]
        public IEnumerable<RewardDto> Rewards { get; set; }
    }

    public class StatDto
    {
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }

        [JsonPropertyName("immature")]
        public decimal Immature { get; set; }
    }

    public class RewardDto
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("reward")]
        public int Reward { get; set; }
    }

    public class PaymentDto
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}