using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    public class PayBolt12OfferResponse
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
