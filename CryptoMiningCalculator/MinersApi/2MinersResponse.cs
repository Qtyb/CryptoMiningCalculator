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

        [JsonPropertyName("paymentsTotal")]
        public decimal TotalPaid { get; set; }

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
}