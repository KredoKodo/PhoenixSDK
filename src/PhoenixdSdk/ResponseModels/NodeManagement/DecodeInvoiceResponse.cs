using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    /// <summary>
    /// Represents the response from the /decodeinvoice endpoint.
    /// </summary>
    public class DecodeInvoiceResponse
    {
        [JsonPropertyName("chain")]
        public required string Chain { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("paymentHash")]
        public required string PaymentHash { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("minFinalCltvExpiryDelta")]
        public int MinFinalCltvExpiryDelta { get; set; }

        [JsonPropertyName("paymentSecret")]
        public required string PaymentSecret { get; set; }

        [JsonPropertyName("paymentMetadata")]
        public required string PaymentMetadata { get; set; }

        [JsonPropertyName("expirySeconds")]
        public int ExpirySeconds { get; set; }

        [JsonPropertyName("extraHops")]
        public List<List<ExtraHop>> ExtraHops { get; set; } = [[]];

        [JsonPropertyName("features")]
        public required Features Features { get; set; }

        [JsonPropertyName("timestampSeconds")]
        public long TimestampSeconds { get; set; }
    }

    public class ExtraHop
    {
        [JsonPropertyName("nodeId")]
        public required string NodeId { get; set; }

        [JsonPropertyName("shortChannelId")]
        public required string ShortChannelId { get; set; }

        [JsonPropertyName("feeBase")]
        public int FeeBase { get; set; }

        [JsonPropertyName("feeProportionalMillionths")]
        public int FeeProportionalMillionths { get; set; }

        [JsonPropertyName("cltvExpiryDelta")]
        public int CltvExpiryDelta { get; set; }
    }

    public class Features
    {
        [JsonPropertyName("activated")]
        public required Dictionary<string, string> Activated { get; set; }

        [JsonPropertyName("unknown")]
        public List<object> Unknown { get; set; } = [];
    }
}