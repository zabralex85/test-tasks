using Newtonsoft.Json;

namespace dotnet_bitcoin_api.Models
{
    public class Transaction
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("from")]
        public string FromAdress { get; set; }

        [JsonProperty("to")]
        public string ToAdress { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
