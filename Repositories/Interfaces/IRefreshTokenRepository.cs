using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetValidAsync(string token, CancellationToken cancellationToken = default);

        Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task<List<RefreshToken>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        void Update(RefreshToken refreshToken);

        void Delete(RefreshToken refreshToken);
    }
}
