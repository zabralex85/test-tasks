namespace dotnet_bitcoin_api.Models
{
    public class WalletInfo
    {
        public string Balance { get; set; }

        public long Blocks { get; set; }

        public int Connections { get; set; }

        public bool TestNet { get; set; }

        public string Proxy { get; set; }

        public string PayTxFee { get; set; }

        public int KeyPoolSize { get; set; }

        public int WalletVersion { get; set; }

        public decimal Difficulty { get; set; }

        public string Error { get; set; }
    }
}
