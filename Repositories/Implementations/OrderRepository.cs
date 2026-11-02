using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddAsync(Order order, 
            CancellationToken cancellationToken)
        {
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
                return order;  
        }
        public async Task<List<Order>> GetAllOrderByUserId(
            int userId,
            CancellationToken cancellationToken)
        {
          return  await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
                .Where(o => o.UserId == userId)
                .ToListAsync(cancellationToken);
                
        }
    }
}
