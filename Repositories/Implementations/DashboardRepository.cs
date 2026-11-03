using Ecommerce.Common.Enum;
using Ecommerce.Data;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class DashboardRepository: IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken)
        {
            // Chỉ tính tổng tiền của các đơn hàng đã thanh toán thành công (PaymentStatus == 1)
            return await _context.Order
                .Where(o => o.PaymentStatus == PaymentStatusType.Paid)
                .SumAsync(o => (decimal)o.GrandTotal, cancellationToken);
        }

        public async Task<int> GetTotalOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.Order.CountAsync(cancellationToken);
        }

        public async Task<int> GetTotalProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Product.CountAsync(cancellationToken);
        }

        public async Task<int> GetTotalUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.CountAsync(cancellationToken);
        }
    }
}
