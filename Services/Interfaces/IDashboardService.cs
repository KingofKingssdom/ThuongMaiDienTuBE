using Ecommerce.Common.Results;
using Ecommerce.DTOs.Dashboard;

namespace Ecommerce.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<Result<DashboardStatsResponse>> GetStatsAsync(CancellationToken cancellationToken);
    }
}
