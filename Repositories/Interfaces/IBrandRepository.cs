using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken = default);
        Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Brand>> GetByCagtegoryIdAsync(int categoryId, 
            CancellationToken cancellationToken = default);
    }
}
