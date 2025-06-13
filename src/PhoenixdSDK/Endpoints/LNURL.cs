using KredoKodo.PhoenixdSDK.Helpers;
using KredoKodo.PhoenixdSDK.ResponseModels.LNURL;
using RestSharp;

namespace KredoKodo.PhoenixdSDK.Endpoints
{
    public class LNURL(PhoenixdClient client)
    {
        /// <summary>
        /// Pays a LNURL-pay resource. Note that the service may apply restrictions on the amount to pay or the message -- the lnurl-pay flow is usually interactive.
        /// </summary>
        /// <param name="amountSat">The amount to pay.</param>
        /// <param name="lnurl">The lnurl-pay resource.</param>
        /// <param name="message">(optional) A comment for the recipient.</param>
        /// <returns>The LNURL service response as a <see cref="PayResponseModel"/> object.</returns>
        public async Task<PayResponseModel> Pay(
            int amountSat,
            string lnurl,
            string? message = null)
        {
            #region Input Validation
            ValidationHelpers.ValidatePositiveValue(amountSat, nameof(amountSat));
            ValidationHelpers.ValidateStringIfNotNull(lnurl, nameof(lnurl));
            #endregion

            var request = new RestRequest("/lnurlpay", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("amountSat", amountSat, ParameterType.GetOrPost);
            request.AddParameter("lnurl", lnurl, ParameterType.GetOrPost);

            if (!string.IsNullOrEmpty(message))
                request.AddParameter("message", message, ParameterType.GetOrPost);

            return await client.ExecuteAsync<PayResponseModel>(request);
        }

        /// <summary>
        /// Withdraws funds from a LNURL service. Phoenixd will withdraw the maximum amount authorized by the service.
        /// </summary>
        /// <param name="lnurl">The lnurl-withdraw resource.</param>
        /// <returns>The LNURL service response as a <see cref="WithdrawResponseModel"/> object.</returns>
        public async Task<WithdrawResponseModel> Withdraw(string lnurl)
        {
            ValidationHelpers.ValidateStringIfNotNull(lnurl, nameof(lnurl));

            var request = new RestRequest("/lnurlwithdraw", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("lnurl", lnurl, ParameterType.GetOrPost);

            return await client.ExecuteAsync<WithdrawResponseModel>(request);
        }

        /// <summary>
        /// Authenticates a LNURL-auth resource to allow an action on a remote service. See LUD-04 for details.
        /// </summary>
        /// <param name="lnurl">The lnurl-auth resource.</param>
        /// <returns>The LNURL service response as a string.</returns>
        public async Task<string> Auth(string lnurl)
        {
            ValidationHelpers.ValidateStringIfNotNull(lnurl, nameof(lnurl));

            var request = new RestRequest("/lnurlwithdraw", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("lnurl", lnurl, ParameterType.GetOrPost);

            return await client.ExecuteRawAsync(request);
        }
    }
}
