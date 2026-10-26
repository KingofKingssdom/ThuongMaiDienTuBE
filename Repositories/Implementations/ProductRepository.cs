using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Product.AddAsync(product,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
       public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Product
                .Include(p=> p.Brand)
                .Include(p=>p.Category)
                .ToListAsync();
        }
       public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Product
                .Include(p=> p.Brand)
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
       public async Task<List<Product>> GetAllByOnPromotion(CancellationToken cancellationToken)
        {
            return await _context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.IsOnPromotion == true)
                .ToListAsync();
        }
       public async Task<List<Product>> GetAllByIsFeatured(
           int categoryId ,
           CancellationToken cancellationToken)
        {
            return await _context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.IsFeatured && p.CategoryId == categoryId)
                .ToListAsync();
        }

    }
}
