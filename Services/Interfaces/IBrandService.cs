using Ecommerce.Common.Results;
using Ecommerce.DTOs.Brand;

namespace Ecommerce.Services.Interfaces
{
    public interface IBrandService
    {
        Task<Result<BrandResponse>> AddAsync(CreateBrandRequest request, CancellationToken cancellationToken);
        Task<Result<BrandResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<List<BrandResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<List<BrandResponse>>> GetAllByCategoryId(
            int categoryId, 
            CancellationToken cancellationToken);
    }
}
