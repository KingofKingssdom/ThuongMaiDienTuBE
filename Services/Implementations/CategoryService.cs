using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.DTOs.Category;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
       public async Task<Result<CategoryResponse>> AddAsync(CreateCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName
            };
            await _categoryRepository.AddCategoryAsync(category, cancellationToken);
            return Result<CategoryResponse>.Success(new CategoryResponse { 
                Id = category.Id,
                CategoryName = category.CategoryName,
                CreatedDay = category.CreatedDay
            }, "Data is created successfully");
        }
       public async Task<Result<List<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categoriesExisting = await _categoryRepository.GetAllCategories(cancellationToken);
            if(categoriesExisting == null)
            {
                return Result<List<CategoryResponse>>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<List<CategoryResponse>>.Success(categoriesExisting.Select(c => new CategoryResponse
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                CreatedDay = c.CreatedDay
            }).ToList());
        }
    }
}
