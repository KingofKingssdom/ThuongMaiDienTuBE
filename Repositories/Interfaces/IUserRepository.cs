using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<User> AddAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task<User> Update(User user, CancellationToken cancellationToken = default);
        Task UdpateUserRoleAsync(int userRoleId, int roleId, CancellationToken cancellationToken = default);
        Task<User> DeleteAsync(
            int userId
            , CancellationToken cancellationToken = default);
        Task<IdentityUserRole<int>> GetUserRoleByUserId(int id, CancellationToken cancellationToken = default);

        Task<User?> GetByIdWithRolesAsync(int id, CancellationToken cancellationToken = default);

        
    }
}
