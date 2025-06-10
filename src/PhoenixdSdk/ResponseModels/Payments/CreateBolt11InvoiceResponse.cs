namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents the response from the /create-bolt11-invoice endpoint.
    /// </summary>
    public class CreateBolt11InvoiceResponse
    {
        public required long AmountSat { get; set; }
        public required string PaymentHash { get; set; }
        public required string Serialized { get; set; }
    }
}