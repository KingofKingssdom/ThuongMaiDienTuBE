using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllByOnPromotion(CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllByIsFeatured(
            int categoryId, 
            CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllByName(
            string productName, 
            CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllByCategoryAndBrand(int categoryId, int? brandId,
            CancellationToken cancellationToken = default);
    }
}
