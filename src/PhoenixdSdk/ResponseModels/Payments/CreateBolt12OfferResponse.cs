namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents the response from the /createoffer endpoint.
    /// </summary>
    public class CreateBolt12OfferResponse
    {
        public required string Offer { get; set; }
    }
}