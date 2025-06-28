namespace KredoKodo.PhoenixdSDK.Helpers
{
    public class Enums
    {
        /// <summary>
        /// Represents the type of proxy used for network requests
        /// </summary>
        public enum ProxyType
        {
            /// <summary>
            /// No proxy is used
            /// </summary>
            None,
            /// <summary>
            /// A standard HTTP proxy is used
            /// </summary>
            Http,
            /// <summary>
            /// A SOCKS5 proxy is used
            /// </summary>
            Socks5
        }
    }
}