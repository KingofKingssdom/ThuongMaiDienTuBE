using Ecommerce.Common.Results;
using Ecommerce.DTOs.Category;

namespace Ecommerce.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryResponse>> AddAsync(CreateCategoryRequest request,
            CancellationToken cancellationToken);
        Task<Result<List<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken);
        
    }
}
