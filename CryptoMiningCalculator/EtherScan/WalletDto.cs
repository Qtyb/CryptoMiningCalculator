using System.Text.Json.Serialization;

namespace CryptoMiningCalculator.EtherScan
{
    public class WalletDto
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}