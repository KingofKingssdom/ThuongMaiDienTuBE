using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class RoleRepository: IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Role> AddAsync(
             Role role,
             CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(
                role,
                cancellationToken);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<bool> ExistsByName(
           string name,
           CancellationToken cancellationToken = default)
        {
            return await _context.Roles.AnyAsync(
                x => x.Name == name);
        }
        public async Task<IEnumerable<IdentityRole<int>>> GetAllAsync(
             CancellationToken cancellationToken = default)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }
        public async Task<IdentityRole<int>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            return role;
        }
        public async Task<IdentityRole<int>> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            string searchName = name.ToUpper();
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == searchName, cancellationToken);
            return role;
        }

        public async Task<IdentityRole<int>> Update(
             int id,
             string name,
             CancellationToken cancellationToken = default)
        {
            var roleExisting = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (roleExisting != null)
            {
                roleExisting.Name = name;
                roleExisting.NormalizedName = name.ToUpper();
                await _context.SaveChangesAsync(cancellationToken);
            }
            return roleExisting;
        }

        public async Task<IdentityUserRole<int>> GetUserRoleByRoleName(
            string name,
            CancellationToken cancellationToken = default)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(
                x => x.NormalizedName == name.ToUpper(),
                cancellationToken);
            return await _context.UserRoles.FirstOrDefaultAsync(
                x => x.RoleId == role.Id);
        }
    }
}
