using Ecommerce.Common.Results;
using Ecommerce.DTOs.User;

namespace Ecommerce.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        //Task<Result<List<UserWithRoleResponse>>> GetAllUserAsync(CancellationToken cancellationToken = default);
        //Task<Result<UserRoleResponse>> AssignRoleTeacherAsync(int id, UpdateUserRoleRequest request, CancellationToken cancellationToken = default);
        //Task<Result<UserResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result<UserResponse>> DeleteAsync(int userId, CancellationToken cancellationToken = default);
        //Task<Result<UserWithRoleResponse>> GetByEmail(string email, CancellationToken cancellationToken = default);
    }
}
