namespace KredoKodo.PhoenixdSDK.ResponseModels.Payments
{
    /// <summary>
    /// Represents an incoming payment.
    /// </summary>
    public class ListIncomingPaymentsResponse
    {
        public ListIncomingPaymentsResponse() { }

        public required string Type { get; set; }
        public required string SubType { get; set; }
        public required string PaymentHash { get; set; }
        public required string Preimage { get; set; }
        public string? ExternalId { get; set; }
        public required string Description { get; set; }
        public required string Invoice { get; set; }
        public bool IsPaid { get; set; }
        public int ReceivedSat { get; set; }
        public int Fees { get; set; }
        public long CompletedAt { get; set; }
        public long CreatedAt { get; set; }
    }
}