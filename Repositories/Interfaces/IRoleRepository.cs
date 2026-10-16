using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> AddAsync(
            Role role,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByName(
            string name,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<IdentityRole<int>>> GetAllAsync(
            CancellationToken cancellationToken = default);
        Task<IdentityRole<int>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task<IdentityRole<int>> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default);
        Task<IdentityRole<int>> Update(
            int id,
            string name,
            CancellationToken cancellationToken = default);

        Task<IdentityUserRole<int>> GetUserRoleByRoleName(
            string name,
            CancellationToken cancellationToken = default);
    }
}
