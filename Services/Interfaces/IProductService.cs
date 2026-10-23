using Ecommerce.Common.Results;
using Ecommerce.DTOs.Product;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<ProductResponse>> AddAsync(CreateProductRequest request, CancellationToken cancellationToken);
        Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<List<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<List<ProductResponse>>> GetAllByIsOnPromotion(CancellationToken cancellationToken);
    }
}
