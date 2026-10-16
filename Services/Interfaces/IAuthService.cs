using Ecommerce.Common.Results;
using Ecommerce.DTOs.User;

namespace Ecommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserResponse>> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        Task<Result> LogoutAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    }
}
