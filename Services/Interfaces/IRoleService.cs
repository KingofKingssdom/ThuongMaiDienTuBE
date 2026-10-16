using Ecommerce.Common.Results;
using Ecommerce.DTOs.Role;

namespace Ecommerce.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Result<RoleResponse>> AddAsync(
            CreateRoleRequest request,
            CancellationToken cancellationToken);
        Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(
            CancellationToken cancellationToken
            );
        Task<Result<RoleResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<RoleResponse>> GetByNameAsync(string name, CancellationToken cancellationToken);
        //Task<Result<RoleResponse>> UpdateAsync(int id, UpdateRoleRequest request, CancellationToken cancellationToken);
    }
}
