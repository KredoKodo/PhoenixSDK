using KredoKodo.PhoenixdSDK.Endpoints;
using KredoKodo.PhoenixdSDK.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace KredoKodo.PhoenixdSDK
{
    /// <summary>
    /// Main client for interacting with the Phoenixd Lightning Network API
    /// </summary>
    public class PhoenixdClient : IDisposable
    {
        private readonly RestClient _restClient;
        private readonly PhoenixdConfiguration _configuration;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the Client
        /// </summary>
        /// <param name="baseUrl">The base URL of the Phoenixd server (e.g., "http://localhost:9740")</param>
        /// <param name="apiPassword">The API password for authentication</param>
        /// <param name="timeout">Optional timeout for requests (default: 30 seconds)</param>
        public PhoenixdClient(string baseUrl, string apiPassword, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base URL cannot be null or empty", nameof(baseUrl));

            if (string.IsNullOrWhiteSpace(apiPassword))
                throw new ArgumentException("API password cannot be null or empty", nameof(apiPassword));

            _configuration = new PhoenixdConfiguration
            {
                BaseUrl = baseUrl.TrimEnd('/'),
                ApiPassword = apiPassword,
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };

            _restClient = CreateRestClient();

            // Expose public endpoints
            NodeManagement = new NodeManagement(this);
            Payments = new Payments(this);
        }

        /// <summary>
        /// Initializes a new instance of the Client using a configuration object
        /// </summary>
        /// <param name="configuration">The Phoenix configuration</param>
        public PhoenixdClient(PhoenixdConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configuration.Validate();
            _restClient = CreateRestClient();
            NodeManagement = new NodeManagement(this);
            Payments = new Payments(this);
        }

        /// <summary>
        /// Gets the current configuration
        /// </summary>
        public PhoenixdConfiguration Configuration => _configuration;

        /// <summary>
        /// Provides access to node management operations
        /// </summary>
        public NodeManagement NodeManagement { get; }

        /// <summary>
        /// Provides access to payment operations
        /// </summary>
        public Payments Payments { get; }

        /// <summary>
        /// Creates and configures the RestSharp client
        /// </summary>
        private RestClient CreateRestClient()
        {
            var options = new RestClientOptions(_configuration.BaseUrl)
            {
                Timeout = _configuration.Timeout,
                ThrowOnAnyError = false,
                ThrowOnDeserializationError = false,
                Authenticator = new HttpBasicAuthenticator("Basic", _configuration.ApiPassword)
            };

            var client = new RestClient(options);

            return client;
        }

        /// <summary>
        /// Executes a request and handles common error scenarios
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="request">The REST request to execute</param>
        /// <returns>The deserialized response</returns>
        internal async Task<T> ExecuteAsync<T>(RestRequest request) where T : class
        {
            ObjectDisposedException.ThrowIf(_disposed, nameof(PhoenixdClient));

            try
            {
                var response = await _restClient.ExecuteAsync<T>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    return response.Data;
                }

                // Handle error scenarios
                var errorMessage = !string.IsNullOrEmpty(response.ErrorMessage)
                    ? response.ErrorMessage
                    : response.Content ?? "Unknown error occurred";

                throw new PhoenixdApiException(
                    $"API request failed: {response.StatusCode}",
                    response.StatusCode,
                    errorMessage,
                    response.ResponseUri?.ToString() ?? "[URI Not Available]"
                );
            }
            catch (PhoenixdApiException)
            {
                throw; // Re-throw our custom exceptions
            }
            catch (Exception ex)
            {
                throw new PhoenixdApiException(
                    "An unexpected error occurred while calling the Phoenixd API",
                    null,
                    ex.Message,
                    request.Resource
                );
            }
        }

        /// <summary>
        /// Executes a request that returns raw string content
        /// </summary>
        /// <param name="request">The REST request to execute</param>
        /// <returns>The raw response content</returns>
        internal async Task<string> ExecuteRawAsync(RestRequest request)
        {
            ObjectDisposedException.ThrowIf(_disposed, nameof(PhoenixdClient));

            try
            {
                var response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return response.Content ?? string.Empty;
                }

                var errorMessage = !string.IsNullOrEmpty(response.ErrorMessage)
                    ? response.ErrorMessage
                    : response.Content ?? "Unknown error occurred";

                throw new PhoenixdApiException(
                    $"API request failed: {response.StatusCode}",
                    response.StatusCode,
                    errorMessage,
                    response.ResponseUri?.ToString() ?? "[URI Not Available]"
                );
            }
            catch (PhoenixdApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PhoenixdApiException(
                    "An unexpected error occurred while calling the Phoenixd API",
                    null,
                    ex.Message,
                    request.Resource
                );
            }
        }

        /// <summary>
        /// Tests the connection to the Phoenixd server
        /// </summary>
        /// <returns>True if the connection is successful, false otherwise</returns>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var request = new RestRequest("/getinfo", Method.Get);
                var response = await _restClient.ExecuteAsync(request);
                return response.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Disposes the client and releases resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method
        /// </summary>
        /// <param name="disposing">True if disposing managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _restClient?.Dispose();
                _disposed = true;
            }
        }
    }
}