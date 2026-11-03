using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
        Task UpdateOrderPaymentStatusAsync(int orderId, int paymentStatus, CancellationToken cancellationToken);
        
    }
}
