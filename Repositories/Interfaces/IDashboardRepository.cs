namespace Ecommerce.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken);
        Task<int> GetTotalOrdersAsync(CancellationToken cancellationToken);
        Task<int> GetTotalProductsAsync(CancellationToken cancellationToken);
        Task<int> GetTotalUsersAsync(CancellationToken cancellationToken);
    }
}
