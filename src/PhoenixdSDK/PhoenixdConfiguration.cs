namespace KredoKodo.PhoenixdSDK
{
    /// <summary>
    /// Configuration class for Phoenixd client
    /// </summary>
    public class PhoenixdConfiguration
    {
        /// <summary>
        /// The base URL of the Phoenixd server
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// The API password for authentication
        /// </summary>
        public string ApiPassword { get; set; } = string.Empty;

        /// <summary>
        /// Timeout for API requests
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Validates the configuration
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new ArgumentException("Base URL cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ApiPassword))
                throw new ArgumentException("API password cannot be null or empty");

            if (Timeout <= TimeSpan.Zero)
                throw new ArgumentException("Timeout must be greater than zero");

            // Ensure BaseUrl doesn't end with a slash for consistency
            BaseUrl = BaseUrl.TrimEnd('/');
        }
    }
}