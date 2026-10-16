using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetValidAsync(
            string token,
            CancellationToken cancellationToken = default)
        {
            return await _context.RefreshToken
                .Include(r => r.User)
                .FirstOrDefaultAsync(x =>
                    x.Token == token &&
                    !x.IsRevoked &&
                    x.ExpiresAt > DateTime.UtcNow,
                    cancellationToken);
        }

        public async Task<RefreshToken> AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken = default)
        {
            await _context.RefreshToken.AddAsync(refreshToken, cancellationToken);

            return refreshToken;
        }

        public async Task<List<RefreshToken>> GetByUserIdAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.RefreshToken
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDay)
                .ToListAsync(cancellationToken);
        }

        public void Update(RefreshToken refreshToken)
        {
            _context.RefreshToken.Update(refreshToken);
        }

        public void Delete(RefreshToken refreshToken)
        {
            _context.RefreshToken.Remove(refreshToken);
        }
    }
}
