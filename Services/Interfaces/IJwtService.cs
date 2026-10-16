using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
