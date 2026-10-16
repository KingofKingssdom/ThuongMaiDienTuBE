using Ecommerce.DTOs.User;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(
                request,
                cancellationToken);

            return this.ToActionResult(result);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(
                request,
                cancellationToken);

            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenAsync(request, cancellationToken);

            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LogoutAsync(
                request,
                cancellationToken);

            return this.ToActionResult(result);
        }
    }
}
