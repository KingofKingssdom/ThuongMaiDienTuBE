namespace Ecommerce.Common.Responses
{
    public class FactoryResponse
    {
        public static ApiResponse<T> Success<T>(
        T data,
        string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ErrorResponse Error(
            string message,
            string? traceId = null)
        {
            return new ErrorResponse
            {
                Message = message,
                TraceId = traceId
            };
        }

        public static ValidationErrorResponse Validation(
            IEnumerable<string> errors)
        {
            return new ValidationErrorResponse
            {
                Message = "Validation failed.",
                Errors = errors
            };
        }
    }
}
