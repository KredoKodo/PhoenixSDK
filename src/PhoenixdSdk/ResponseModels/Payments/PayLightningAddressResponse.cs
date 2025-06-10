using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents the response from the /paylnaddress endpoint.
    /// </summary>
    public class PayLightningAddressResponse
    {
        [JsonPropertyName("recipientAmountSat")]
        public int RecipientAmountSat { get; set; }

        [JsonPropertyName("routingFeeSat")]
        public int RoutingFeeSat { get; set; }

        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; } = string.Empty;

        [JsonPropertyName("paymentHash")]
        public string PaymentHash { get; set; } = string.Empty;

        [JsonPropertyName("paymentPreimage")]
        public string PaymentPreimage { get; set; } = string.Empty;
    }
}