namespace KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement
{
    /// <summary>
    /// Represents the response from the Phoenixd server's /getinfo endpoint
    /// </summary>
    public class GetInfoResponse
    {
        public required string NodeId { get; set; }
        public int BlockHeight { get; set; }
        public required string Version { get; set; }
        public required string Chain { get; set; }

        public List<Channel> Channels { get; set; } = [];

        public class Channel
        {
            public required string State { get; set; }
            public required string ChannelId { get; set; }
            public long BalanceSat { get; set; }
            public long InboundLiquiditySat { get; set; }
            public long CapacitySat { get; set; }
            public required string FundingTxId { get; set; }
        }
    }
}