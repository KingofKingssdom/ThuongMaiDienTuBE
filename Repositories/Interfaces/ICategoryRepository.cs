using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task<List<Category>> GetAllCategories(CancellationToken cancellationToken = default);
        
    }
}
