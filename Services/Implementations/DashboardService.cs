using Ecommerce.Common.Results;
using Ecommerce.DTOs.Dashboard;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class DashboardService: IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<Result<DashboardStatsResponse>> GetStatsAsync(CancellationToken cancellationToken)
        {
            var revenue = await _dashboardRepository.GetTotalRevenueAsync(cancellationToken);
            var orders = await _dashboardRepository.GetTotalOrdersAsync(cancellationToken);
            var products = await _dashboardRepository.GetTotalProductsAsync(cancellationToken);
            var users = await _dashboardRepository.GetTotalUsersAsync(cancellationToken);

            return Result<DashboardStatsResponse>.Success(new DashboardStatsResponse
            {
                TotalRevenue = revenue,
                TotalOrders = orders,
                TotalProducts = products,
                TotalUsers = users
            });
        }
    }
}
