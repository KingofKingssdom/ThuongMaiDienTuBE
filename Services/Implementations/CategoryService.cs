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
       public async Task<Result<CategoryResponse>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var categoriesExisting = await _categoryRepository.GetById(id, cancellationToken);
            if (categoriesExisting == null)
            {
                return Result<CategoryResponse>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<CategoryResponse>.Success( new CategoryResponse
            {
                Id = categoriesExisting.Id,
                CategoryName = categoriesExisting.CategoryName,
                CreatedDay = categoriesExisting.CreatedDay
            });
        }
       public async Task<Result<CategoryResponse>> UpdateAsync(
            int id,
            CreateCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var categoriesExisting = await _categoryRepository.GetById(id, cancellationToken);
            if (categoriesExisting == null)
            {
                return Result<CategoryResponse>.Failure("Not found", ErrorType.NotFound);
            }
            categoriesExisting.CategoryName = request.CategoryName;
            await _categoryRepository.UpdateAsync(cancellationToken);
            return Result<CategoryResponse>.Success(new CategoryResponse
            {
                Id = categoriesExisting.Id,
                CategoryName = categoriesExisting.CategoryName,
                CreatedDay = categoriesExisting.CreatedDay
            },"Data updated successfully");
        }
       public async Task<Result<CategoryResponse>> DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            var categoriesExisting = await _categoryRepository.GetById(id, cancellationToken);
            if (categoriesExisting == null)
            {
                return Result<CategoryResponse>.Failure("Not found", ErrorType.NotFound);
            }
            categoriesExisting.DeteledDay = DateTime.UtcNow;
            await _categoryRepository.UpdateAsync(cancellationToken);
             return Result<CategoryResponse>.Success(new CategoryResponse
            {
                Id = categoriesExisting.Id,
                CategoryName = categoriesExisting.CategoryName,
                CreatedDay = categoriesExisting.CreatedDay
            }, "Data deleted successfully");
        }
    }
}
