namespace Ecommerce.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
