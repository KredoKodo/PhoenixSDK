using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents a single outgoing payment.
    /// </summary>
    public class ListOutgoingPaymentsResponse
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("subType")]
        public required string SubType { get; set; }

        [JsonPropertyName("paymentId")]
        public required string PaymentId { get; set; }

        [JsonPropertyName("txId")]
        public required string? TxId { get; set; }

        [JsonPropertyName("paymentHash")]
        public required string? PaymentHash { get; set; }

        [JsonPropertyName("preimage")]
        public required string? Preimage { get; set; }

        [JsonPropertyName("isPaid")]
        public bool IsPaid { get; set; }

        [JsonPropertyName("sent")]
        public long Sent { get; set; }

        [JsonPropertyName("fees")]
        public long Fees { get; set; }

        [JsonPropertyName("invoice")]
        public required string? Invoice { get; set; }

        [JsonPropertyName("completedAt")]
        public long CompletedAt { get; set; }

        [JsonPropertyName("createdAt")]
        public long CreatedAt { get; set; }
    }
}