using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class CartItemRepository: ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CartItem> AddAsync(CartItem cartItem, 
            CancellationToken cancellationToken)
        {
            await _context.CartItem.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<CartItem?> FindCartItemExisting(int productVariantId, int cartId,
            CancellationToken cancellationToken)
        {
            return await _context.CartItem
                .FirstOrDefaultAsync(c => 
                c.ProductVariantId == productVariantId
                && c.CartId == cartId, 
                cancellationToken);
        }
        public async Task<CartItem> UpdateAsync(CartItem cartItem,
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<List<CartItem>> GetAllByCartIdAsync(int cartId,
            CancellationToken cancellationToken)
        {
            return await _context.CartItem
                 .Where(c => c.CartId == cartId)
                 .Include(c => c.ProductVariant)
                 .ThenInclude(p=>p.Product)
                .ToListAsync();
        }
        public async Task UpdatePriceAtTimeAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
