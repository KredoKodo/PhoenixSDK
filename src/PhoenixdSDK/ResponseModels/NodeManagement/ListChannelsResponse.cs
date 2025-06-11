using System.Text.Json.Serialization;

namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    /// <summary>
    /// Represents a single channel in the /listchannels response.
    /// </summary>
    public class ListChannelsResponse
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("commitments")]
        public required Commitments Commitments { get; set; }

        [JsonPropertyName("shortChannelId")]
        public required string ShortChannelId { get; set; }

        [JsonPropertyName("channelUpdate")]
        public required ChannelUpdate ChannelUpdate { get; set; }
    }

    public class Commitments
    {
        [JsonPropertyName("params")]
        public required CommitmentParams Params { get; set; }
    }

    public class CommitmentParams
    {
        [JsonPropertyName("channelId")]
        public required string ChannelId { get; set; }

        [JsonPropertyName("channelConfig")]
        public required string[] ChannelConfig { get; set; }

        [JsonPropertyName("channelFeatures")]
        public required string[] ChannelFeatures { get; set; }

        [JsonPropertyName("localParams")]
        public required LocalParams LocalParams { get; set; }

        [JsonPropertyName("remoteParams")]
        public required RemoteParams RemoteParams { get; set; }

        [JsonPropertyName("channelFlags")]
        public int ChannelFlags { get; set; }
    }

    public class LocalParams
    {
        [JsonPropertyName("nodeId")]
        public required string NodeId { get; set; }

        [JsonPropertyName("fundingKeyPath")]
        public required string FundingKeyPath { get; set; }

        [JsonPropertyName("dustLimit")]
        public long DustLimit { get; set; }

        [JsonPropertyName("maxHtlcValueInFlightMsat")]
        public long MaxHtlcValueInFlightMsat { get; set; }

        [JsonPropertyName("htlcMinimum")]
        public long HtlcMinimum { get; set; }

        [JsonPropertyName("toSelfDelay")]
        public int ToSelfDelay { get; set; }

        [JsonPropertyName("maxAcceptedHtlcs")]
        public int MaxAcceptedHtlcs { get; set; }

        [JsonPropertyName("isInitiator")]
        public bool IsInitiator { get; set; }
    }

    public class RemoteParams
    {
        [JsonPropertyName("nodeId")]
        public required string NodeId { get; set; }

        [JsonPropertyName("dustLimit")]
        public long DustLimit { get; set; }

        [JsonPropertyName("maxHtlcValueInFlightMsat")]
        public long MaxHtlcValueInFlightMsat { get; set; }

        [JsonPropertyName("htlcMinimum")]
        public long HtlcMinimum { get; set; }
    }

    public class ChannelUpdate
    {
        [JsonPropertyName("signature")]
        public required string Signature { get; set; }

        [JsonPropertyName("chainHash")]
        public required string ChainHash { get; set; }

        [JsonPropertyName("shortChannelId")]
        public required string ShortChannelId { get; set; }

        [JsonPropertyName("timestampSeconds")]
        public long TimestampSeconds { get; set; }

        [JsonPropertyName("messageFlags")]
        public int MessageFlags { get; set; }

        [JsonPropertyName("channelFlags")]
        public int ChannelFlags { get; set; }

        [JsonPropertyName("cltvExpiryDelta")]
        public int CltvExpiryDelta { get; set; }

        [JsonPropertyName("htlcMinimumMsat")]
        public long HtlcMinimumMsat { get; set; }

        [JsonPropertyName("feeBaseMsat")]
        public long FeeBaseMsat { get; set; }

        [JsonPropertyName("feeProportionalMillionths")]
        public long FeeProportionalMillionths { get; set; }

        [JsonPropertyName("htlcMaximumMsat")]
        public long HtlcMaximumMsat { get; set; }
    }
}