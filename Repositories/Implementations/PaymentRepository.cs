using Ecommerce.Common.Enum;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Order.FindAsync(new object[] { orderId }, cancellationToken);
        }

        public async Task UpdateOrderPaymentStatusAsync(int orderId, int paymentStatus, CancellationToken cancellationToken)
        {
            var order = await _context.Order.FindAsync( orderId , cancellationToken);
            if (order != null)
            {
                order.PaymentStatus = PaymentStatusType.Paid; 
                _context.Order.Update(order);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
