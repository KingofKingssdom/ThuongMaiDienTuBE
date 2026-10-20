using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class CartRepository: ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository (AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task<Cart?> GetByUserId(int userId,
            CancellationToken cancellationToken)
        {
            return await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }
    }
}
