namespace Ecommerce.Common.Responses
{
    public class ErrorResponse
    {
        public bool Success => false;

        public string Message { get; init; } = string.Empty;

        public string? TraceId { get; init; }

        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
