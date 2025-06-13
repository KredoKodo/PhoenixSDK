using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.LNURL
{
    /// <summary>
    /// Represents the response from the /lnurlpay endpoint.
    /// </summary>
    public class PayResponseModel
    {
        [JsonPropertyName("recipientAmountSat")]
        public int RecipientAmountSat { get; set; }

        [JsonPropertyName("routingFeeSat")]
        public int RoutingFeeSat { get; set; }

        [JsonPropertyName("paymentId")]
        public required string PaymentId { get; set; }

        [JsonPropertyName("paymentHash")]
        public required string PaymentHash { get; set; }

        [JsonPropertyName("paymentPreimage")]
        public required string PaymentPreimage { get; set; }
    }
}
