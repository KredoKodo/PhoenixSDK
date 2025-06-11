using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents the response from the /payinvoice endpoint.
    /// </summary>
    public class Bolt11PayInvoiceResponse
    {
        [JsonPropertyName("recipientAmountSat")]
        public long RecipientAmountSat { get; set; }

        [JsonPropertyName("routingFeeSat")]
        public long RoutingFeeSat { get; set; }

        [JsonPropertyName("paymentId")]
        public required string PaymentId { get; set; }

        [JsonPropertyName("paymentHash")]
        public required string PaymentHash { get; set; }

        [JsonPropertyName("paymentPreimage")]
        public required string PaymentPreimage { get; set; }
    }
}