using Newtonsoft.Json;

namespace dotnet_bitcoin_api.Models
{
    public class SendQuery
    {
        [JsonProperty("to")]
        public string ToAddress { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
