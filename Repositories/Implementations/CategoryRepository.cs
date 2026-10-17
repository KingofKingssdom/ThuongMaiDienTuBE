using Azure.Core;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
       public async Task<List<Category>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await _context.Category.ToListAsync(cancellationToken);

        }
       
    }
}
