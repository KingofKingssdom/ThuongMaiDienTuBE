using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    public class OrderItemRepository: IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
           
        }
    }
}
