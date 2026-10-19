using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<ProductVariant> AddAsync(
            ProductVariant productVariant, 
            CancellationToken cancellationToken = default);
        Task<ProductVariant?> GetByIdAsync(int id,
            CancellationToken cancellationToken = default);
        Task<List<ProductVariant>> GetAllAsync(
            CancellationToken cancellationToken = default);

    }
}
