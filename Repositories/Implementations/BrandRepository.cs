using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class BrandRepository : IBrandRepository 
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken)
        {
            await _context.Brand.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }
       public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Brand.FirstOrDefaultAsync(b => b.Id == id);
        }
       public async Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Brand.ToListAsync(cancellationToken);
        }
       public async Task<List<Brand>> GetByCagtegoryIdAsync(int categoryId,
            CancellationToken cancellationToken)
        {
            return await _context.Brand
                  .Where(b => b.CategoryBrand
                     .Any(cb => cb.CategoryId == categoryId))
                .ToListAsync(cancellationToken);
        }
       public async Task UpdateAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
      
    }
}
