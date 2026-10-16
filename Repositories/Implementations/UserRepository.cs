using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u=> u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync(cancellationToken);
        }
        public async Task<User?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {

            var user = await _context.Users
             .Include(u => u.UserRoles)
             .ThenInclude(ur => ur.Role)
             .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user is null) return null;

            return user;
        }

        public async Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AnyAsync(
                    x => x.Email == email,
                    cancellationToken);
        }

        public async Task<User> AddAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(
                user,
                cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            // Mỗi khi role được tạo ra nó sẽ có role mặc định là USER
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == "USER", cancellationToken);
            if (role != null)
            {
                await _context.UserRoles.AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return user;
        }

        public async Task<User> Update(User user, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> DeleteAsync(
            int userId,
            CancellationToken cancellationToken)
        {
            var userExisting = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userExisting != null)
            {
                userExisting.IsActive = false;
                _context.Update(userExisting);
                await _context.SaveChangesAsync();
            }
            return userExisting;
        }
        public async Task UdpateUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken = default)
        {
            var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId, cancellationToken);
            if (existingUserRole != null)
            {
                _context.UserRoles.Remove(existingUserRole);
            }
            await _context.UserRoles.AddAsync(new UserRole
            {
                UserId = userId,
                RoleId = roleId
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
        public async Task<IdentityUserRole<int>?> GetUserRoleByUserId(
             int id,
             CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(
                u => u.UserId == id,
                cancellationToken);

        }
        public async Task<User?> GetByIdWithRolesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        
    }
}
