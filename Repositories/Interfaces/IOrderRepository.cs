using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllOrderByUserId(
            int userId, 
            CancellationToken cancellationToken = default);
    }
}
