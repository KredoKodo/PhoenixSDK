using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    /// <summary>
    /// Represents the response from the /estimate-liquidity-fees endpoint.
    /// </summary>
    public class EstimateLiquidityFeesResponse
    {
        [JsonPropertyName("miningFeeSat")]
        public int MiningFeeSat { get; set; }

        [JsonPropertyName("serviceFeeSat")]
        public int ServiceFeeSat { get; set; }
    }
}