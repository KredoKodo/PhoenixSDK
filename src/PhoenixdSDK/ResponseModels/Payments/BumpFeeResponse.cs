namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents the response from the /bump-fee endpoint.
    /// </summary>
    public class BumpFeeResponse
    {
        public required string Status { get; set; }
        public required string TxId { get; set; }
        public long FeesPaid { get; set; }
    }
}