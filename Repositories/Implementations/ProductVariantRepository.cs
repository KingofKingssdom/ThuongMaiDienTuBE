using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class ProductVariantRepository: IProductVariantRepository
    {
        private readonly AppDbContext _context;
        public ProductVariantRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<ProductVariant> AddAsync(
            ProductVariant productVariant,
            CancellationToken cancellationToken)
        {
            await _context.ProductVariant.AddAsync(productVariant);
            await _context.SaveChangesAsync();
            return productVariant;
        }
       public async Task<ProductVariant?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.ProductVariant
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
       public async Task<List<ProductVariant>> GetAllAsync(
            CancellationToken cancellationToken)
        {
           return await _context.ProductVariant
                .Include(p => p.Product)
                .ToListAsync();
        }
    }
}
