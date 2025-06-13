using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.LNURL
{
    /// <summary>
    /// Represents the response from the /lnurlwithdraw endpoint.
    /// </summary>
    public class WithdrawResponseModel
    {
        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("minWithdrawable")]
        public int MinWithdrawable { get; set; }

        [JsonPropertyName("maxWithdrawable")]
        public int MaxWithdrawable { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("k1")]
        public required string K1 { get; set; }

        [JsonPropertyName("invoice")]
        public required string Invoice { get; set; }
    }
}
