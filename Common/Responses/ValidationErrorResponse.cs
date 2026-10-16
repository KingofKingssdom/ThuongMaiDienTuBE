namespace Ecommerce.Common.Responses
{
    public class ValidationErrorResponse: ErrorResponse
    {
        public IEnumerable<string> Errors { get; init; }
                = Enumerable.Empty<string>();
    }
}
