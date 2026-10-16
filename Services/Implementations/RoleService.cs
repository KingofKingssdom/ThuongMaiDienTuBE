using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.Data;
using Ecommerce.DTOs.Role;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly AppDbContext _context;
        public RoleService(
            IRoleRepository roleRepository,
            AppDbContext context)
        {
            _context = context;
            _roleRepository = roleRepository;
        }
        public async Task<Result<RoleResponse>> AddAsync(
             CreateRoleRequest request,
             CancellationToken cancellationToken)
        {
            if (await _roleRepository.ExistsByName(request.Name, cancellationToken))
            {
                return Result<RoleResponse>.Failure(
                    "Role name already exists",
                    ErrorType.Conflict);
            }
            var role = new Role
            {
                Name = request.Name,
                NormalizedName = request.Name.ToUpperInvariant()
            };
            await _roleRepository.AddAsync(role, cancellationToken);
            return Result<RoleResponse>.Success(new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            }, "Role is succeed");
        }
        public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            if (roles == null)
            {
                return Result<IEnumerable<RoleResponse>>.Failure("Role not found ", ErrorType.NotFound);
            }
            return Result<IEnumerable<RoleResponse>>.Success(roles.Select(role => new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            }).ToList());
        }

        public async Task<Result<RoleResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
            {
                return Result<RoleResponse>.Failure($"Role not found with Id = {id}", ErrorType.NotFound);
            }
            return Result<RoleResponse>.Success(new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            }, $"Get role with ID = {role.Id} succeed");

        }
        public async Task<Result<RoleResponse>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByNameAsync(name, cancellationToken);
            if (role == null)
            {
                return Result<RoleResponse>.Failure($"Role not found with role name = {name}", ErrorType.NotFound);
            }
            return Result<RoleResponse>.Success(new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            }, $"Get role with role name = {role.Name} succeed");
        }
        //public async Task<Result<RoleResponse>> UpdateAsync(int id, UpdateRoleRequest request, CancellationToken cancellationToken)
        //{
        //    var role = await _roleRepository.Update(id, request.Name, cancellationToken);
        //    return Result<RoleResponse>.Success(new RoleResponse
        //    {
        //        Id = role.Id,
        //        Name = role.Name
        //    }, $"Update role with role name = {role.Name} succeed");
        //}
    }
}
