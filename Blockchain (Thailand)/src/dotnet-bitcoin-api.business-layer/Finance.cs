using BitcoinLib.Services.Coins.Bitcoin;

namespace dotnet_bitcoin_api.business_layer
{

    public class Finance
    {
        IBitcoinService BitcoinService { get; set; }

        public Finance(short rpcRequestTimeoutInSeconds, bool useTestnet)
        {
            BitcoinService = new BitcoinService(rpcRequestTimeoutInSeconds, useTestnet);

            /*
             * if (string.IsNullOrWhiteSpace(DaemonUrl)
                    || string.IsNullOrWhiteSpace(RpcUsername)
                    || string.IsNullOrWhiteSpace(RpcPassword)) */

            
            //BitcoinService.Parameters.DaemonUrl = ConfigurationManager.AppSettings.Get("Bitcoin_DaemonUrl");
            //BitcoinService.Parameters.DaemonUrlTestnet = "";
            //BitcoinService.Parameters.RpcUsername = "";
            //BitcoinService.Parameters.RpcPassword = "";
            //BitcoinService.Parameters.WalletPassword = useTestnet;
        }

        public decimal GetBalance()
        {
            var networkDifficulty = BitcoinService.GetDifficulty();
            var myBalance = BitcoinService.GetBalance();

            return myBalance;
        }
    }
}
