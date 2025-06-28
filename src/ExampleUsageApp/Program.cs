using KredoKodo.PhoenixdSDK;
using System.Text.Json;

namespace ExampleUsageApp
{
    class Program
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
        };

        static async Task Main()
        {
            /*

            This is an example usage of the Phoenixd SDK client.

            It demonstrates how to connect to a Phoenixd server, retrieve node information,
            check the connection status, and perform various operations such as getting balance,
            listing channels, closing a channel, decoding an invoice, and creating a BOLT11 invoice.

            NOTE: You'll need to replace the base URL and API password with your own Phoenixd server details.
            In addition, all code that interacts with the server is commented out to prevent accidental execution.

            */

            var client = new PhoenixdClient("address-goes-here", "password-goes-here");

            // Example: Test if connection to the server is successful
             bool isConnected = await client.TestConnectionAsync();
            if (!isConnected)
            {
                Console.WriteLine("Failed to connect to the Phoenixd server.");
            }

            Console.WriteLine("Connection successful: " + isConnected);

            #region Node Management Examples

            //Example: Get node info
            //var nodeInfo = await client.NodeManagement.GetInfoAsync();
            //Console.WriteLine("Node Info: " + JsonSerializer.Serialize(nodeInfo, _jsonOptions));

            //Example: Get balance
            //var balance = await client.NodeManagement.GetBalanceAsync();
            //Console.WriteLine("Balance (Sats): " + balance.BalanceSat);

            //Example: List channels
            //var channels = await client.NodeManagement.ListChannelsAsync();
            //Console.WriteLine("Channels: " + JsonSerializer.Serialize(channels, _jsonOptions));

            //Example: Close a channel (replace with a valid information)
            //Note: Closing a channel is final and cannot be cancelled.
            //This example is commented out because it requires valid channel ID and address.
            //var closeResponse = client.NodeManagement.CloseChannelAsync("channelId-goes-here", "address-goes-here", 10);
            //Console.WriteLine("Close Channel Response: " + closeResponse);

            //Example: Decode an invoice
            //var decodedInvoice = await client.NodeManagement.DecodeInvoiceAsync(invoice.Serialized);
            //Console.WriteLine("Decoded Invoice: " + JsonSerializer.Serialize(decodedInvoice, _jsonOptions));

            //Example: Decode an offer
            //var decodedOffer = await client.NodeManagement.DecodeBolt12OfferAsync(offer);
            //Console.WriteLine("Decoded Offer: " + JsonSerializer.Serialize(decodedOffer, _jsonOptions));

            //Example: Estimate liquidity fees
            //var liquidityFees = await client.NodeManagement.EstimateLiquidityFeesAsync(1000);
            //Console.WriteLine("Decoded Offer: " + JsonSerializer.Serialize(liquidityFees, _jsonOptions));

            #endregion

            #region Payments Examples

            //Example: Create a BOLT11 invoice
            //var invoice = await client.Payments.CreateBolt11InvoiceAsync(1, "Test Invoice");
            //Console.WriteLine("Created Invoice: " + JsonSerializer.Serialize(invoice, _jsonOptions));

            //Example: Create a BOLT12 Offer
            //var offer = await client.Payments.CreateBolt12OfferAsync("Send me a donation!");
            //Console.WriteLine("Created Offer: " + offer);

            //Example: Pay a BOTL11 Invoice
            //Note: Not going to run this as it requires a valid invoice and funds.
            //var paymentResult = await client.Payments.PayBolt11InvoiceAsync(invoice.Serialized, 1);
            //Console.WriteLine("Payment Result: " + JsonSerializer.Serialize(paymentResult, _jsonOptions));

            //Example: Get a lightning address
            //Note: This will return an error message as no channels are open.
            //var lightningAddress = await client.Payments.GetLightningAddressAsync();
            //Console.WriteLine("Lightning Address: " + lightningAddress);

            //Example: Pay a BOLT12 Offer
            //Note: This is commented out so as not to actually try to pay.
            //var offerPaymentResult = await client.Payments.PayBolt12OfferAsync(offer, message:"Please stack these SATS!");
            //Console.WriteLine("Offer Payment Result: " + JsonSerializer.Serialize(offerPaymentResult, _jsonOptions));

            //Example: Pay lightning address
            //Note: This is commented out as it requires a valid lightning address and funds.
            //var lightningPaymentResult = await client.Payments.PayLightningAddressAsync(lightningAddress, 1, "Test payment to lightning address");
            //Console.WriteLine("Lightning Payment Result: " + JsonSerializer.Serialize(lightningPaymentResult, _jsonOptions));

            //Example: Pay on-chain address
            //Note: This is commented out as it requires a valid on-chain address and funds.
            //var onChainPaymentResult = await client.Payments.PayOnchainAddressAsync(1000, "bc1qxyz...", 1);
            //Console.WriteLine("On-Chain Payment Result: " + JsonSerializer.Serialize(onChainPaymentResult, _jsonOptions));

            //Example: Bump fee for a payment
            //Note: This is commented out as there are no payments to bump.
            //var bumpFeeResult = await client.Payments.BumpFeeAsync(10);
            //Console.WriteLine("Bump Fee Result: " + JsonSerializer.Serialize(bumpFeeResult, _jsonOptions));

            //Example: List incoming payments
            //var incomingPayments = await client.Payments.ListIncomingPaymentsAsync(all: true);
            //Console.WriteLine("Incoming Payments: " + JsonSerializer.Serialize(incomingPayments, _jsonOptions));

            //Example: Get incoming payment by payment ID or hash
            //Note: Replace "b02a9a090c7a5ae7af39f4c8398629fd596d348a452a48d290a8e250bcdd7f31" with a valid payment hash.
            //This example is commented out because it requires a valid payment hash.
            //var incomingPayment = await client.Payments.GetIncomingPaymentAsync("b02a9a090c7a5ae7af39f4c8398629fd596d348a452a48d290a8e250bcdd7f31");
            //Console.WriteLine("Incoming Payment: " + JsonSerializer.Serialize(incomingPayment, _jsonOptions));

            //Example: Display all outgoing payments
            //var outgoingPayments = await client.Payments.ListOutgoingPaymentsAsync(all: true);
            //Console.WriteLine("Outgoing Payments: " + JsonSerializer.Serialize(outgoingPayments, _jsonOptions));

            //Example: Get outgoing payment by payment ID or hash
            //Note: Replace "a1b2c3d4-e5f6-7890-abcd-ef1234567890" with a valid payment ID.
            //This example will return an empty array because it requires a valid payment ID.
            //var outgoingPayment = await client.Payments.GetOutgoingPaymentAsync(paymentId: "a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            //Console.WriteLine("Outgoing Payment: " + JsonSerializer.Serialize(outgoingPayment, _jsonOptions));

            //Example: CVS Exporting successful payment history to a CSV file
            //var csvExport = await client.Payments.ExportPaymentsCsvAsync();
            //Note: You would typically write the csvExport content to a file here.
            //Since there is no payment history in this example, it will return a NotFound code.

            #endregion

            #region LNURL Examples
            
            // Example: Pay a LNURL-pay resource
            //var lnurlPayResponse = await client.LNURL.Pay(1, "lnurl-pay-resource-url-goes-here", "Test payment");
            //Console.WriteLine("LNURL Pay Response: " + JsonSerializer.Serialize(lnurlPayResponse, _jsonOptions));

            // Example: Withdraw from a LNURL service
            //var lnurlWithdraw = await client.LNURL.Withdraw("lnurl-withdraw-service-url-goes-here");
            //Console.WriteLine("LNURL Withdraw Service Response: " + JsonSerializer.Serialize(lnurlWithdraw, _jsonOptions));

            // Example: Authenticate with a LNURL-auth service
            //var lnurlAuthResponse = await client.LNURL.Auth("lnurl-auth-service-url-goes-here");
            //Console.WriteLine("LNURL Auth Response: " + lnurlAuthResponse);

            #endregion

        }
    }
}