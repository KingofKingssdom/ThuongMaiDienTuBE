using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(
            CancellationToken cancellationToken = default);
    }
}
