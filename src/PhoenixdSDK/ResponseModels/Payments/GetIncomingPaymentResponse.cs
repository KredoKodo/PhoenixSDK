using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents a single incoming payment.
    /// </summary>
    public class GetIncomingPaymentResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("subType")]
        public string SubType { get; set; } = string.Empty;

        [JsonPropertyName("paymentHash")]
        public string PaymentHash { get; set; } = string.Empty;

        [JsonPropertyName("preimage")]
        public string Preimage { get; set; } = string.Empty;

        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("invoice")]
        public string Invoice { get; set; } = string.Empty;

        [JsonPropertyName("isPaid")]
        public bool IsPaid { get; set; }

        [JsonPropertyName("receivedSat")]
        public int ReceivedSat { get; set; }

        [JsonPropertyName("fees")]
        public int Fees { get; set; }

        [JsonPropertyName("completedAt")]
        public long CompletedAt { get; set; }

        [JsonPropertyName("createdAt")]
        public long CreatedAt { get; set; }
    }
}