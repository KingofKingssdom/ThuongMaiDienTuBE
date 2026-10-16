using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.Data;
using Ecommerce.DTOs.User;
using Ecommerce.Repositories.Interface;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;
        public UserService(
            IUserRepository userRepository,
            AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<Result<UserResponse>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
            {
                return Result<UserResponse>.Failure("User not found.", ErrorType.NotFound);
            }

            return Result<UserResponse>.Success(new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedDay = user.CreatedDay
            }, $"Get user with ID = {user.Id} successful");
        }


        //public async Task<Result> UpdateRoleAsync(
        //    int id,
        //    UpdateUserRoleRequest request,
        //    CancellationToken cancellationToken = default)
        //{
        //    var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        //    if (user is null)
        //    {
        //        return Result.Failure(
        //            "User not found.",
        //            ErrorType.NotFound);
        //    }

        //    //user.Role = request.Role;

        //    _userRepository.Update(user);

        //    await _context.SaveChangesAsync(cancellationToken);

        //    return Result.Success("Role updated successfully.");
        //}
        //public async Task<Result<List<UserWithRoleResponse>>> GetAllUserAsync(
        //    CancellationToken cancellationToken = default)
        //{
        //    var users = await _userRepository.GetAllAsync(cancellationToken);
        //    if (users is null)
        //    {
        //        return Result<List<UserWithRoleResponse>>.Failure("User not found.", ErrorType.NotFound);
        //    }
        //    return Result<List<UserWithRoleResponse>>.Success(users.Select(user => new UserWithRoleResponse
        //    {
        //        Id = user.Id,
        //        FullName = user.FullName,
        //        Email = user.Email!,
        //        IsActive = user.IsActive,
        //        CreatedAt = user.CreatedAt,
        //        RoleName = user.UserRoles
        //        .Select(ur => ur.Role.NormalizedName)
        //               .FirstOrDefault() ?? string.Empty,
        //        RoleRequirement = user.RoleRequirement
        //        .OrderByDescending(r => r.Id)
        //        .Select(rr => new RoleRequirementResponse
        //        {
        //            Id = rr.Id,
        //            Status = rr.Status,
        //            Requirement = rr.Requirement
        //        }).FirstOrDefault()
        //    }).ToList(),

        //    "Successfully retrieved all users");
        //}
        //public async Task<Result<UserResponse>> UpdateAsync(int id,
        //     UpdateUserRequest request,
        //     CancellationToken cancellationToken)
        //{
        //    var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        //    if (user == null)
        //    {
        //        return Result<UserResponse>.Failure($"Not found user with ID = {id}", ErrorType.NotFound);
        //    }
        //    if (request.FullName != null || request.FullName != "")
        //    {
        //        user.FullName = request.FullName;
        //    }
        //    if (request.Email != null || request.Email != "")
        //    {
        //        user.Email = request.Email;
        //    }
        //    await _userRepository.Update(user, cancellationToken);
        //    return Result<UserResponse>.Success(new UserResponse
        //    {
        //        Id = user.Id,
        //        FullName = user.FullName,
        //        Email = user.Email,
        //        IsActive = user.IsActive
        //    }, $"Update user with ID = {id} succeed");
        //}
        public async Task<Result<UserResponse>> DeleteAsync(int userId, CancellationToken cancellationToken = default)
        {
            var userExisting = await _userRepository.DeleteAsync(userId);
            return Result<UserResponse>.Success(new UserResponse
            {
                Id = userExisting.Id,
                FullName = userExisting.FullName,
                Email = userExisting.Email,
                IsActive = userExisting.IsActive,
                CreatedDay = userExisting.CreatedDay
            }, $"Delete user with ID = {userExisting.Id} successful");
        }
        //public async Task<Result<UserRoleResponse>> AssignRoleTeacherAsync(
        //    int id,
        //    UpdateUserRoleRequest request,
        //    CancellationToken cancellationToken = default)
        //{
        //    var userExisting = await _userRepository.GetUserRoleByUserId(id, cancellationToken);
        //    if (userExisting == null)
        //    {
        //        return Result<UserRoleResponse>.Failure(
        //            $"Not found with userId = {id}",
        //            ErrorType.NotFound);
        //    }
        //    var roleExisting = await _roleRepository.GetByNameAsync(request.NormalizedName);
        //    if (roleExisting == null)
        //    {
        //        return Result<UserRoleResponse>.Failure(
        //            $"Not found with role name = {request.NormalizedName}",
        //            ErrorType.NotFound);
        //    }
        //    await _userRepository.UdpateUserRoleAsync(id, roleExisting.Id, cancellationToken);
        //    if (roleExisting.NormalizedName == "ADMIN")
        //    {
        //        return Result<UserRoleResponse>.Success(new UserRoleResponse
        //        {
        //            RoleName = roleExisting.Name
        //        }, $"Update role with role name = {roleExisting.Name} succeed");
        //    }

        //    var user = await _userRepository.GetByIdAsync(userExisting.UserId, cancellationToken);
        //    var teacherExisting = await _teacherRepository.GetByUserId(user.Id, cancellationToken);
        //    if (teacherExisting == null)
        //    {
        //        var teacher = new Teacher
        //        {
        //            Fullname = user.FullName,
        //            Email = user.Email,
        //            UserId = user.Id
        //        };
        //        await _teacherRepository.AddTeacher(teacher, cancellationToken);
        //        await _roleRequirementService.ApprovedRole(user.Id, cancellationToken);
        //    }
        //    else if (teacherExisting != null)
        //    {
        //        return Result<UserRoleResponse>.Failure("teacher already", ErrorType.Validation);
        //    }
        //    return Result<UserRoleResponse>.Success(new UserRoleResponse
        //    {
        //        RoleName = roleExisting.Name
        //    }, $"Update role with role name = {roleExisting.Name} succeed");

        //}
        //public async Task<Result<UserWithRoleResponse>> GetByEmail(string email, CancellationToken cancellationToken)
        //{
        //    var userExisting = await _userRepository.GetByEmailAsync(email, cancellationToken);
        //    if (userExisting is null)
        //    {
        //        return Result<UserWithRoleResponse>.Failure("User not found.", ErrorType.NotFound);
        //    }

        //    return Result<UserWithRoleResponse>.Success(new UserWithRoleResponse
        //    {
        //        Id = userExisting.Id,
        //        Email = userExisting.Email,
        //        FullName = userExisting.FullName,
        //        IsActive = userExisting.IsActive,
        //        RoleName = userExisting.UserRoles
        //        .Select(ur => ur.Role.NormalizedName)
        //               .FirstOrDefault() ?? string.Empty
        //    }, $"Get user with email = {userExisting.Email} successful");
        //}
    }
}
