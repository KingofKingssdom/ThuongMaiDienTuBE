using Ecommerce.Common.Results;
using Ecommerce.DTOs.ProductVariant;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductVariantService
    {
        Task<Result<ProductVariantResponse>> AddAsync(
            CreateProductVariantRequest request,
            CancellationToken cancellationToken);
        Task<Result<ProductVariantResponse>> GetByIdAsync(
            int id, CancellationToken cancellationToken);
        Task<Result<List<ProductVariantResponse>>> GetAllAsync(
            CancellationToken cancellationToken);
        Task<Result<ProductVariantResponse>> UpdateAsync(
            int id,
            CreateProductVariantRequest request,
            CancellationToken cancellationToken);
        Task<Result<ProductVariantResponse>> DeleteAsync(
            int id,
            CancellationToken cancellationToken);

        Task<Result<List<ProductVariantResponse>>> GetByProductIdAsync(
            int productId,
            CancellationToken cancellationToken);
    }
}
