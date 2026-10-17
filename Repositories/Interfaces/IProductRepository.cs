using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
