using KredoKodo.PhoenixdSDK.Helpers;
using KredoKodo.PhoenixdSDK.ResponseModels.NodeManagement;
using RestSharp;

namespace KredoKodo.PhoenixdSDK.Endpoints
{
    public class NodeManagement(PhoenixdClient client)
    {

        /// <summary>
        /// Gets information about the Phoenixd node.
        /// </summary>
        /// <returns>Node information as a <see cref="GetInfoResponse"/> object.</returns>
        public async Task<GetInfoResponse> GetInfoAsync()
        {
            var request = new RestRequest("/getinfo", Method.Get);
            return await client.ExecuteAsync<GetInfoResponse>(request);
        }

        /// <summary>
        /// Gets the balance information of the Phoenixd node.
        /// </summary>
        /// <returns>Balance information as a JSON object.</returns>
        public async Task<GetBalanceResponse> GetBalanceAsync()
        {
            var request = new RestRequest("/getbalance", Method.Get);
            return await client.ExecuteAsync<GetBalanceResponse>(request);
        }

        /// <summary>
        /// Gets the list of channels for the Phoenixd node.
        /// </summary>
        /// <returns>List of channels as a <see cref="ListChannelsResponse"/> object.</returns>
        public async Task<List<ListChannelsResponse>> ListChannelsAsync()
        {
            var request = new RestRequest("/listchannels", Method.Get);
            return await client.ExecuteAsync<List<ListChannelsResponse>>(request);
        }

        /// <summary>
        /// Closes a given channel, and send all funds to an on-chain address. Returns the ID of the closing transaction.
        /// Attention: closing a channel is final, it cannot be cancelled.
        /// </summary>
        /// <param name="channelId">Identifier of the channel to close.</param>
        /// <param name="address">Bitcoin address where your balance will be sent to.</param>
        /// <param name="feerateSatByte">Fee rate in satoshi per vbyte.</param>
        /// <returns>Result of the close operation as a <see cref="CloseChannelResponse"/> object.</returns>
        public async Task<CloseChannelResponse> CloseChannelAsync(
            string channelId,
            string address,
            int feerateSatByte)
        {
            #region Input Validation
            ValidationHelpers.ValidateStringIfNotNull(channelId, nameof(channelId));
            ValidationHelpers.ValidateStringIfNotNull(address, nameof(address));
            #endregion

            if (feerateSatByte <= 0)
                throw new ArgumentException("Fee rate must be positive", nameof(feerateSatByte));

            var request = new RestRequest("/closechannel", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request
                .AddParameter("channelId", channelId, ParameterType.GetOrPost)
                .AddParameter("address", address, ParameterType.GetOrPost)
                .AddParameter("feerateSatByte", feerateSatByte, ParameterType.GetOrPost);

            return await client.ExecuteAsync<CloseChannelResponse>(request);
        }

        /// <summary>
        /// Decodes a Lightning invoice.
        /// </summary>
        /// <param name="invoice">The BOLT11 invoice string to decode.</param>
        /// <returns>Decoded invoice information as a <see cref="DecodeInvoiceResponse"/> object.</returns>
        public async Task<DecodeInvoiceResponse> DecodeInvoiceAsync(string invoice)
        {
            ValidationHelpers.ValidateStringIfNotNull(invoice, nameof(invoice));

            var request = new RestRequest("/decodeinvoice", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("invoice", invoice, ParameterType.GetOrPost);

            return await client.ExecuteAsync<DecodeInvoiceResponse>(request);
        }

        /// <summary>
        /// Estimates a liquidity fee for a given amount. Note that it depends on the current mining feerate, which is volatile. The estimate returned is the full cost and does not take into account any fee credit you may have.
        /// </summary>
        /// <param name="amountSat">The liquidity amount in satoshi.</param>
        /// <returns>Estimated liquidity fees as a <see cref="EstimateLiquidityFeesResponse"/> object.</returns>
        public async Task<EstimateLiquidityFeesResponse> EstimateLiquidityFeesAsync(int amountSat)
        {
            ValidationHelpers.ValidatePositiveValue(amountSat, nameof(amountSat));

            var request = new RestRequest($"/estimateliquidityfees?amountSat={amountSat}", Method.Get);

            return await client.ExecuteAsync<EstimateLiquidityFeesResponse>(request);
        }

        /// <summary>
        /// Decodes a BOLT12 offer. This is useful to understand the offer details before paying it.
        /// </summary>
        /// <param name="offer">The BOLT12 offer string to decode.</param>
        /// <returns>Decoded invoice information as a <see cref="DecodeBolt12OfferResponse"/> object.</returns>
        public async Task<DecodeBolt12OfferResponse?> DecodeBolt12OfferAsync(string offer)
        {
            ValidationHelpers.ValidateStringIfNotNull(offer, nameof(offer));

            var request = new RestRequest("/decodeoffer", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("offer", offer, ParameterType.GetOrPost);

            return await client.ExecuteAsync<DecodeBolt12OfferResponse>(request);
        }
    }
}