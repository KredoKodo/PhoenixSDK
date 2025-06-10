namespace KredoKodo.PhoenixdSDK.Exceptions
{
    /// <summary>
    /// Custom exception for Phoenixd API errors
    /// </summary>
    public class PhoenixdApiException : Exception
    {
        /// <summary>
        /// The HTTP status code returned by the API
        /// </summary>
        public System.Net.HttpStatusCode? StatusCode { get; }

        /// <summary>
        /// The raw error response from the API
        /// </summary>
        public string ApiResponse { get; }

        /// <summary>
        /// The endpoint that was called when the error occurred
        /// </summary>
        public string Endpoint { get; }

        /// <summary>
        /// Initializes a new instance of PhoenixdApiException
        /// </summary>
        public PhoenixdApiException(string message, System.Net.HttpStatusCode? statusCode = null, string apiResponse = "", string endpoint = "")
            : base(message)
        {
            StatusCode = statusCode;
            ApiResponse = apiResponse ?? string.Empty;
            Endpoint = endpoint ?? string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of PhoenixdApiException with inner exception
        /// </summary>
        public PhoenixdApiException(string message, Exception innerException, System.Net.HttpStatusCode? statusCode = null, string apiResponse = "", string endpoint = "")
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ApiResponse = apiResponse ?? string.Empty;
            Endpoint = endpoint ?? string.Empty;
        }
    }
}