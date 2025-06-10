using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    public class DecodeBolt12OfferResponse
    {
        [JsonPropertyName("chain")]
        public string Chain { get; set; } = string.Empty;

        [JsonPropertyName("chainHashes")]
        public List<string> ChainHashes { get; set; } = new List<string>();

        [JsonPropertyName("path")]
        public List<BlindedPath> Path { get; set; } = new List<BlindedPath>();
    }

    public class BlindedPath
    {
        [JsonPropertyName("introductionNodeId")]
        public IntroductionNodeId IntroductionNodeId { get; set; } = new IntroductionNodeId();

        [JsonPropertyName("blindingKey")]
        public string BlindingKey { get; set; } = string.Empty;

        [JsonPropertyName("blindedNodes")]
        public List<BlindedNode> BlindedNodes { get; set; } = new List<BlindedNode>();
    }

    public class IntroductionNodeId
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; } = string.Empty;
    }

    public class BlindedNode
    {
        [JsonPropertyName("blindedPublicKey")]
        public string BlindedPublicKey { get; set; } = string.Empty;

        [JsonPropertyName("encryptedPayload")]
        public string EncryptedPayload { get; set; } = string.Empty;
    }
}
