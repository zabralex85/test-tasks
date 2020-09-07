using Newtonsoft.Json;

namespace dotnet_bitcoin_api.Models
{
    public class Wallet
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}
