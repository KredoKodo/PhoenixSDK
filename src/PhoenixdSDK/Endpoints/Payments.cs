using KredoKodo.PhoenixdSDK.ResponseModels.Payments;
using RestSharp;

namespace KredoKodo.PhoenixdSDK.Endpoints
{
    public class Payments(PhoenixdClient client)
    {
        /// <summary>
        /// Creates a new BOLT11 invoice.
        /// </summary>
        /// <param name="description">The description of the invoice (max. 128 characters)</param>
        /// <param name="amountSat">(optional) The amount requested by the invoice, in satoshi. If not set, the invoice can be paid by any amount.</param>
        /// <param name="expirySeconds">(optional) The invoice expiry in seconds, by default 3600 (1 hour).</param>
        /// <param name="externalId">(optional) A custom identifier. Use that to link the invoice to an external system.</param>
        /// <param name="webhookUrl">(optional) A webhook url that will be notified when this specific payment has been received. This notification is done in addition to the normal webhooks defined in the configuration. This webhook is authenticated.</param>
        /// <returns>The created invoice as a <see cref="CreateBolt11InvoiceResponse"/> object.</returns>
        public async Task<CreateBolt11InvoiceResponse> CreateBolt11InvoiceAsync(
            string description,
            long? amountSat = null,
            long? expirySeconds = null,
            string? externalId = null,
            string? webhookUrl = null)
        {
            #region Input Validation
            ValidatePositiveValue(amountSat, nameof(amountSat));
            EnsureNotNullOrWhiteSpace(description, nameof(description));

            if (description.Length > 128)
                throw new ArgumentException("Description is too long", nameof(description));

            ValidatePositiveValue(expirySeconds, nameof(expirySeconds));

            if (externalId != null && string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentException("External ID cannot be empty when specified", nameof(externalId));

            if (webhookUrl != null)
            {
                if (string.IsNullOrWhiteSpace(webhookUrl))
                    throw new ArgumentException("Webhook URL cannot be empty when specified", nameof(webhookUrl));

                if (!Uri.TryCreate(webhookUrl, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != "http" && uri.Scheme != "https"))
                    throw new ArgumentException("Webhook URL must be a valid HTTP/HTTPS URL", nameof(webhookUrl));
            }
            #endregion

            var request = new RestRequest("/createinvoice", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "amountSat", amountSat },
                { "description", description },
                { "expirySeconds", expirySeconds },
                { "externalId", externalId },
                { "webhookUrl", webhookUrl }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteAsync<CreateBolt11InvoiceResponse>(request);
        }

        /// <summary>
        /// Creates a Bolt12 offer with an optional description and amount.
        /// </summary>
        /// <param name="description">(optional) The description of the offer (max. 128 characters).</param>
        /// <param name="amountSat">(optional) The amount requested by the offer, in satoshi. If not set, the offer can be paid by any amount.</param>
        /// <returns>The created offer as a <see cref="CreateBolt12OfferResponse"/> object.</returns>
        /// <remarks>
        /// An offer is a static and reusable payment request that does not expire. It can be paid many times. It's well suited for donations or tips.
        /// Note: a getoffer call is also available but it is deprecated. This getoffer endpoint returns an offer, but always the same one. createoffer should be used instead.
        /// </remarks>
        public async Task<string> CreateBolt12OfferAsync(
            string? description = null,
            int? amountSat = null)
        {
            #region Input Validation
            ValidatePositiveValue(amountSat, nameof(amountSat));

            if (description != null)
            {
                if (string.IsNullOrWhiteSpace(description))
                    throw new ArgumentException("Description cannot be empty when specified", nameof(description));

                if (description.Length > 128)
                    throw new ArgumentException("Description is too long", nameof(description));
            }
            #endregion

            var request = new RestRequest("/createoffer", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "description", description },
                { "amountSat", amountSat }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteRawAsync(request);
        }

        /// <summary>
        /// Pays a BOLT11 Lightning invoice. A 0.4% fee applies. Response includes the internal paymentId for that payment.
        /// </summary>
        /// <param name="invoice">BOLT11 invoice.</param>
        /// <param name="amountSat">(optional) Amount in satoshi. If unset, will pay the amount requested in the invoice.</param>
        /// <returns>The payment result as a <see cref="Bolt11PayInvoiceResponse"/> object.</returns>
        public async Task<Bolt11PayInvoiceResponse> PayBolt11InvoiceAsync(
            string invoice,
            int? amountSat = null)
        {
            EnsureNotNullOrWhiteSpace(invoice, nameof(invoice));
            ValidatePositiveValue(amountSat, nameof(amountSat));

            var request = new RestRequest("/payinvoice", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("invoice", invoice, ParameterType.GetOrPost);

            if (amountSat.HasValue && amountSat.Value <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0.", nameof(amountSat));
            }
            else if (amountSat.HasValue)
            {
                request.AddParameter("amountSat", amountSat.Value, ParameterType.GetOrPost);
            }

            return await client.ExecuteAsync<Bolt11PayInvoiceResponse>(request);
        }

        /// <summary>
        /// Pays a BOLT12 Lightning offer. A 0.4% fee applies. Response includes the internal paymentId for that payment.
        /// </summary>
        /// <param name="offer">BOLT12 offer to pay.</param>
        /// <param name="amountSat">(optional) Amount in satoshi. If unset, will pay the amount requested in the invoice.</param>
        /// <param name="message">(optional) A message for the recipient.</param>
        /// <returns>The payment result as a <see cref="PayBolt12OfferResponse"/> object.</returns>
        public async Task<PayBolt12OfferResponse> PayBolt12OfferAsync(
            string offer,
            int? amountSat = null,
            string? message = null)
        {
            #region Input Validation
            EnsureNotNullOrWhiteSpace(offer, nameof(offer));
            EnsureNotNullOrWhiteSpace(message, nameof(message));
            ValidatePositiveValue(amountSat, nameof(amountSat));

            if (message!.Length > 128)
                throw new ArgumentException("Message is too long", nameof(message));
            #endregion

            var request = new RestRequest("/payoffer", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "offer", offer },
                { "amountSat", amountSat },
                { "message", message }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteAsync<PayBolt12OfferResponse>(request);
        }

        /// <summary>
        /// Pays an email-like Lightning address, either based on BIP-353 or LNURL. A 0.4% fee applies. Response includes the internal paymentId for that payment.
        /// </summary>
        /// <param name="address">The Lightning Address to pay (e.g., user@domain.com).</param>
        /// <param name="amountSat">(optional) Amount in satoshi. If unset, will pay the amount requested in the invoice.</param>
        /// <param name="message">(optional) A message for the recipient.</param>
        /// <returns>The payment result as a <see cref="PayLightningAddressResponse"/> object.</returns>
        public async Task<PayLightningAddressResponse> PayLightningAddressAsync(
            string address,
            int? amountSat = null,
            string? message = null)
        {
            #region Input Validation
            EnsureNotNullOrWhiteSpace(address, nameof(address));
            ValidatePositiveValue(amountSat, nameof(amountSat));
            EnsureNotNullOrWhiteSpace(message, nameof(message));
            
            if (message!.Length > 128)
                throw new ArgumentException("Message is too long", nameof(message));
            #endregion

            var request = new RestRequest("/paylnaddress", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "address", address },
                { "amountSat", amountSat },
                { "message", message }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteAsync<PayLightningAddressResponse>(request);
        }

        /// <summary>
        /// Sends part of your current balance to a Bitcoin address. The spliced channel is not closed and remains active. Returns the transaction id if the splice was successful.
        /// </summary>
        /// <param name="amountSat">Amount in satoshi.</param>
        /// <param name="address">Bitcoin address where funds will be sent.</param>
        /// <param name="feerateSatByte">Fee rate in satoshi per vbyte.</param>
        /// <returns>The payment result as a string.</returns>
        public async Task<string> PayOnchainAddressAsync(
            int amountSat,
            string address,
            int feerateSatByte)
        {
            #region Input Validation
            ValidatePositiveValue(amountSat, nameof(amountSat));
            EnsureNotNullOrWhiteSpace(address, nameof(address));
            ValidatePositiveValue(feerateSatByte, nameof(feerateSatByte));
            #endregion

            var request = new RestRequest("/sendtoaddress", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("amountSat", amountSat, ParameterType.GetOrPost);
            request.AddParameter("address", address, ParameterType.GetOrPost);
            request.AddParameter("feerateSatByte", feerateSatByte, ParameterType.GetOrPost);

            return await client.ExecuteRawAsync(request);
        }

        /// <summary>
        /// Makes all your unconfirmed transactions use a higher fee rate, using CPFP. Returns the ID of the child transaction.
        /// </summary>
        /// <param name="feerateSatByte">Fee rate in satoshi per vbyte.</param>
        /// <returns>The bump fee result as a string.</returns>
        public async Task<string> BumpFeeAsync(int feerateSatByte)
        {
            ValidatePositiveValue(feerateSatByte, nameof(feerateSatByte));

            var request = new RestRequest("/bumpfee", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("feerateSatByte", feerateSatByte, ParameterType.GetOrPost);

            return await client.ExecuteRawAsync(request);
        }

        /// <summary>
        /// Lists all incoming payments.
        /// </summary>
        /// <param name="from">Start timestamp in millis from epoch, default 0</param>
        /// <param name="to">End timestamp in millis from epoch, default now</param>
        /// <param name="limit">Number of payments in the page, default 20</param>
        /// <param name="offset">Page offset, default 0</param>
        /// <param name="all">Also return unpaid invoices</param>
        /// <param name="externalId">Only include payments that use this external id.</param>
        /// <returns>A list of incoming payments as a <see cref="ListIncomingPaymentsResponse"/> object.</returns>
        public async Task<List<ListIncomingPaymentsResponse>> ListIncomingPaymentsAsync(
            int? from = null,
            int? to = null,
            int? limit = null,
            int? offset = null,
            bool? all = null,
            string? externalId = null)
        {
            #region Input Validation
            ValidateNonNegativeValue(from, nameof(from));
            ValidateNonNegativeValue(to, nameof(to));
            
            if (from.HasValue && to.HasValue && from.Value > to.Value)
                throw new ArgumentException("From timestamp cannot be greater than to timestamp");

            ValidatePositiveValue(limit, nameof(limit));
            ValidateNonNegativeValue(offset, nameof(offset));
            EnsureNotNullOrWhiteSpace(externalId, nameof(externalId));
            #endregion

            var request = new RestRequest("/payments/incoming", Method.Get);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "from", from },
                { "to", to },
                { "limit", limit },
                { "offset", offset },
                { "all", all },
                { "externalId", externalId }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteAsync<List<ListIncomingPaymentsResponse>>(request);
        }

        /// <summary>
        /// Gets details for specific incoming payment by the payment hash.
        /// </summary>
        /// <param name="paymentHash">A payment hash to look up.</param>
        /// <returns>A an incoming payment as a <see cref="GetIncomingPaymentResponse"/> object.</returns>
        public async Task<GetIncomingPaymentResponse> GetIncomingPaymentAsync(string paymentHash)
        {
            EnsureNotNullOrWhiteSpace(paymentHash, nameof(paymentHash));

            if (string.IsNullOrWhiteSpace(paymentHash))
            {
                throw new ArgumentException("Payment hash cannot be null or empty.", nameof(paymentHash));
            }

            var request = new RestRequest($"/payments/incoming/{paymentHash}", Method.Get);

            return await client.ExecuteAsync<GetIncomingPaymentResponse>(request);
        }

        /// <summary>
        /// Lists all outgoing payments.
        /// </summary>
        /// <param name="from">Start timestamp in millis from epoch, default 0.</param>
        /// <param name="to">End timestamp in millis from epoch, default now.</param>
        /// <param name="limit">Number of payments in the page, default 20.</param>
        /// <param name="offset">Page offset, default 0p.</param>
        /// <param name="all">Also return payments that have failed.</param>
        /// <returns>A list of outgoing payments as a <see cref="ListOutgoingPaymentsResponse"/> object.</returns>
        public async Task<List<ListOutgoingPaymentsResponse>> ListOutgoingPaymentsAsync(
            int? from = null,
            int? to = null,
            int? limit = null,
            int? offset = null,
            bool? all = null)
        {
            #region Input Validation
            ValidateNonNegativeValue(from, nameof(from));
            ValidateNonNegativeValue(to, nameof(to));
            
            if (from.HasValue && to.HasValue && from.Value > to.Value)
                throw new ArgumentException("From timestamp cannot be greater than to timestamp");

            ValidatePositiveValue(limit, nameof(limit));
            ValidateNonNegativeValue(offset, nameof(offset));
            #endregion

            var request = new RestRequest("/payments/outgoing", Method.Get);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, object?>()
            {
                { "from", from },
                { "to", to },
                { "limit", limit },
                { "offset", offset },
                { "all", all }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteAsync<List<ListOutgoingPaymentsResponse>>(request);
        }

        /// <summary>
        /// Gets details for a specific outgoing payment by its payment id or hash.
        /// </summary>
        /// <param name="paymentId">The payment ID to look up.</param>
        /// <param name="paymentHash">The payment hash to look up.</param>
        /// <returns>The outgoing payment details as a <see cref="GetOutgoingPaymentResponse"/> object.</returns>
        public async Task<GetOutgoingPaymentResponse> GetOutgoingPaymentAsync(
            string? paymentId = null,
            string? paymentHash = null)
        {
            EnsureNotNullOrWhiteSpace(paymentId, nameof(paymentId));
            EnsureNotNullOrWhiteSpace(paymentHash, nameof(paymentHash));

            var endpoint = (paymentId, paymentHash) switch
            {
                (not null, null) => $"/payments/outgoing/{paymentId}",
                (null, not null) => $"/payments/outgoing/hash/{paymentHash}",
                _ => throw new ArgumentException("Exactly one of paymentId or paymentHash must be provided.")
            };

            var request = new RestRequest(endpoint, Method.Get);
            return await client.ExecuteAsync<GetOutgoingPaymentResponse>(request);
        }

        /// <summary>
        /// Exports your successful payments history in a CSV file. Returns the path of the generated file on your file system.
        /// </summary>
        /// <param name="from">Start timestamp in millis from epoch, default 0.</param>
        /// <param name="to">End timestamp in millis from epoch, default now.</param>
        /// <returns>The CSV export as a string.</returns>
        /// <remarks>The resulting CSV allows precise tracking of the balance and fee credit, and shows the split between mining and service fees:
        /// - balance: sum of all amount_msat;
        /// - fee credit: sum of all fee_credit_msat.
        /// </remarks>
        public async Task<string> ExportPaymentsCsvAsync(int? from = null, int? to = null)
        {
            #region Input Validation
            ValidateNonNegativeValue(from, nameof(from));
            ValidateNonNegativeValue(to, nameof(to));
            
            if (from.HasValue && to.HasValue && from.Value > to.Value)
                throw new ArgumentException("From timestamp cannot be greater than to timestamp");
            #endregion

            var request = new RestRequest("/export", Method.Get);

            var parameters = new Dictionary<string, object?>()
            {
                { "from", from },
                { "to", to }
            };

            foreach (var param in parameters)
            {
                if (param.Value is not null)
                {
                    request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
            }

            return await client.ExecuteRawAsync(request);
        }

        /// <summary>
        /// Gets a BIP-353 Lightning address from the LSP. Only works if you have a channel.
        /// </summary>
        /// <returns>Returns a BIP-353 Lightning address.</returns>
        /// <remarks>Note you can also use third-party services or self-host the address.</remarks>
        public async Task<string> GetLightningAddressAsync()
        {
            var request = new RestRequest("/getlnaddress", Method.Get);
            return await client.ExecuteRawAsync(request);
        }

        #region Input Validation Helpers
        private static void ValidatePositiveValue(int? value, string paramName)
        {
            if (value.HasValue && value.Value <= 0)
                throw new ArgumentException($"{paramName} must be positive when specified", paramName);
        }

        private static void ValidatePositiveValue(long? value, string paramName)
        {
            if (value.HasValue && value.Value <= 0)
                throw new ArgumentException($"{paramName} must be positive when specified", paramName);
        }

        static void EnsureNotNullOrWhiteSpace(string? value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be null or empty", paramName);
        }

        private static void ValidateNonNegativeValue(int? value, string paramName)
        {
            if (value.HasValue && value.Value < 0)
                throw new ArgumentException($"{paramName} must be non-negative when specified", paramName);
        }
        #endregion
    }
}