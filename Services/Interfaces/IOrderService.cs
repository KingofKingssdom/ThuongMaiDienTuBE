using Ecommerce.Common.Results;
using Ecommerce.DTOs.Order;

namespace Ecommerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderResponse>> AddAsync(
            int userId,
            CreateOrderRequest request,
            CancellationToken cancellationToken);
    }
}
