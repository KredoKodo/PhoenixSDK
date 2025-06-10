namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    /// <summary>
    /// Represents the response from the Phoenixd server's /get-balance endpoint
    /// </summary>
    public class GetBalanceResponse
    {
        public long BalanceSat { get; set; }
        public long FeeCreditSat { get; set; }
    }
}