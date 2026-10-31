using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

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
        Task UpdateStockAsync(
            CancellationToken cancellationToken = default);
        Task UpdateAsync(CancellationToken cancellationToken = default);
        Task<List<ProductVariant>> GetByProductId(
             int productId,
             CancellationToken cancellationToken = default);
        
    }
}
