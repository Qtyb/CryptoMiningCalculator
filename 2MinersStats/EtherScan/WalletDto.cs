using System.Text.Json.Serialization;

namespace _2MinersStats.EtherScan
{
    public class WalletDto
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}