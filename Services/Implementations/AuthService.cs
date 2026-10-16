using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.Data;
using Ecommerce.DTOs.User;
using Ecommerce.Models;
using Ecommerce.Repositories.Interface;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly AppDbContext _context;
        private readonly ILogger<AuthService> _logger;
        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService,
            AppDbContext context,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<UserResponse>> RegisterAsync(
            CreateUserRequest request,
            CancellationToken cancellationToken = default)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                return Result<UserResponse>.Failure(
                    "Email already exists.",
                    ErrorType.Conflict);
            }

            var user = new User
            {
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user, cancellationToken);

            _logger.LogInformation("User registered: {Email}", user.Email);

            return Result<UserResponse>.Success(new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedDay = user.CreatedDay
            }, "Register is succeed");
        }

        public async Task<Result<LoginResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken);

            if (user is null)
            {
                return Result<LoginResponse>.Failure(
                    "Email or password is incorrect.",
                    ErrorType.Unauthorized);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Result<LoginResponse>.Failure(
                    "Email or password is incorrect.",
                    ErrorType.Unauthorized);
            }
            var accessToken = _jwtService.GenerateToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(
                new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                },
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);


            return Result<LoginResponse>.Success(
                new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    User = new UserResponse
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        CreatedDay = user.CreatedDay
                    }
                }, "Login is succeed");
        }

        public async Task<Result<TokenResponse>> RefreshTokenAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            var refreshToken = await _refreshTokenRepository.GetValidAsync(request.RefreshToken, cancellationToken);

            if (refreshToken is null)
            {
                return Result<TokenResponse>.Failure("Refresh token is invalid.", ErrorType.Unauthorized);
            }

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            _refreshTokenRepository.Update(refreshToken);

            var user = await _userRepository.GetByEmailAsync(
        refreshToken.User.Email,
       cancellationToken);


            var accessToken = _jwtService.GenerateToken(user);

            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(
                new RefreshToken
                {
                    UserId = refreshToken.UserId,
                    Token = newRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                },
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<TokenResponse>.Success(
                new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                }, "The token was created successfully");
        }

        public async Task<Result> LogoutAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            var refreshToken = await _refreshTokenRepository.GetValidAsync(request.RefreshToken, cancellationToken);

            if (refreshToken is null)
            {
                return Result.Failure(
                    "Refresh token not found.",
                    ErrorType.NotFound);
            }

            if (refreshToken.IsRevoked)
            {
                return Result.Failure(
                    "Refresh token has already been revoked.",
                    ErrorType.Conflict);
            }

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            _refreshTokenRepository.Update(refreshToken);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "User {UserId} logged out.",
                refreshToken.UserId);

            return Result.Success("Logout successfully.");
        }
    }
}
