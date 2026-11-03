using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController: ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats(CancellationToken cancellationToken)
        {
                var result = await _dashboardService.GetStatsAsync(cancellationToken);
                return this.ToActionResult(result);
        }
    }
}
