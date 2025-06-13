namespace KredoKodo.PhoenixdSDK.Helpers
{
    public static class ValidationHelpers
    {
        public static void ValidatePositiveValue(int? value, string paramName)
        {
            if (value.HasValue && value.Value <= 0)
                throw new ArgumentException($"{paramName} must be positive when specified", paramName);
        }

        public static void ValidatePositiveValue(long? value, string paramName)
        {
            if (value.HasValue && value.Value <= 0)
                throw new ArgumentException($"{paramName} must be positive when specified", paramName);
        }

        public static void EnsureNotNullOrWhiteSpace(string? value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be null or empty", paramName);
        }

        public static void ValidateNonNegativeValue(int? value, string paramName)
        {
            if (value.HasValue && value.Value < 0)
                throw new ArgumentException($"{paramName} must be non-negative when specified", paramName);
        }
    }
}