using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart,
            CancellationToken cancellationToken = default);
        Task<Cart?> GetByUserId(int userId, 
            CancellationToken cancellationToken = default);
    }
}
