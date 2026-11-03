namespace Ecommerce.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateVNPayUrlAsync(int orderId, HttpContext httpContext);
        Task<(bool isSuccess, int orderId)> ProcessVNPayCallbackAsync(IQueryCollection queryParams, CancellationToken cancellationToken);
    }
}
